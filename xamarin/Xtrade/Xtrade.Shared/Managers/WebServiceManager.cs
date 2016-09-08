namespace Xtrade.Shared.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Converters;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Managers;
    using Newtonsoft.Json;
    using Utilities;

    public class WebServiceManager : IWebServiceManager
    {
        private const string ResponseExceptionMessage = "The web service returned an invalid response";

        public async Task<IBaseResponse<T>> GetAndParse<T>(string url, JsonConverter[] customConverters)
        {
            IBaseResponse<T> response = BootStrapper.Resolve<IBaseResponse<T>>();

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(Configuration.MobileApiUrl + url);
                webRequest.Method = "GET";
                webRequest.Accept = "application/json, text/json";

#if __ANDROID__
                webRequest.UserAgent = "Xtrade-android";
#else
                webRequest.UserAgent = "Xtrade-iOS";
#endif

                webRequest.Timeout = Configuration.TimeoutInterval;
                webRequest.ReadWriteTimeout = Configuration.TimeoutInterval;

                this.GenerateHttpHeaders(webRequest);
                HttpWebResponse webResponse = null;
                await Task.Run(() => { webResponse = (HttpWebResponse) webRequest.GetResponse(); });

                if (webResponse != null && webResponse.StatusCode == HttpStatusCode.OK)
                {
                    response.Result = await this.DeflateAndParseContent<T>(webResponse, customConverters, null);
                }
            }
            catch (ResponseException re)
            {
                SetResponseErrors(response, ResponseExceptionMessage, re.Code);
            }
            catch (WebException we)
            {
                HttpWebResponse responseException = we.Response as HttpWebResponse;

                if (responseException != null)
                {
                    if (responseException.StatusCode == HttpStatusCode.BadRequest || responseException.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        SetResponseErrors(response, "Not Authorised", CommonConstants.UnauthorisedErrorCode);
                    }
                    else
                    {
                        SetResponseErrors(response, "Web Service call failed", CommonConstants.ResponseNotOkErrorCode + (int) responseException.StatusCode);
                    }
                }
                else
                {
                    SetResponseErrors(response, we.Message, CommonConstants.WebServiceExceptionErrorCode);
                }
            }
            catch (Exception ex)
            {
                SetResponseErrors(response, ex.Message, CommonConstants.GeneralExceptionErrorCode);
            }

            return response;
        }

        private void GenerateHttpHeaders(HttpWebRequest webRequest)
        {
            Dictionary<string, string> headerValues = new Dictionary<string, string>();
            string authenticationToken = "";

            if (!string.IsNullOrWhiteSpace(authenticationToken))
            {
                headerValues.Add("apikey", authenticationToken);
            }
            else
            {
                throw new WebException("Authentication Token is null!", WebExceptionStatus.SendFailure);
            }

            if (headerValues.Count > 0)
            {
                foreach (KeyValuePair<string, string> headerValue in headerValues.Where(headerValue => !string.IsNullOrWhiteSpace(headerValue.Value)))
                {
                    webRequest.Headers[headerValue.Key] = headerValue.Value;
                }
            }
        }

        private async Task<T> DeflateAndParseContent<T>(HttpWebResponse webResponse, JsonConverter[] customConverters, HttpResponseMessage responseMessage)
        {
            if (webResponse == null && responseMessage == null)
            {
                throw new WebException("Invalid Web Response and Response Message!");
            }

            string responseString = string.Empty;

            if (webResponse != null)
            {
                Stream responseStream = webResponse.GetResponseStream();
                responseStream.ReadTimeout = Configuration.TimeoutInterval;

                if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }

                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                responseString = await reader.ReadToEndAsync();
                responseStream.Close();

                if (string.IsNullOrWhiteSpace(responseString) && webResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    webResponse.Close();
                    return BootStrapper.Resolve<T>();
                }

                webResponse.Close();
            }
            else
            {
                responseString = await responseMessage.Content.ReadAsStringAsync();
            }

            if (!string.IsNullOrEmpty(responseString))
            {
                T response;

                try
                {
                    response = JsonConvert.DeserializeObject<T>(responseString, GenerateJsonSettings<T>(customConverters));

                    if (response == null)
                    {
                        throw new ResponseException("Parse Error - response is null", CommonConstants.ResponseParseError);
                    }
                }
                catch (Exception ex)
                {
                    throw new ResponseException("Parse Error - " + ex.Message, CommonConstants.ResponseParseError);
                }

                return response;
            }

            throw new ResponseException("Response content is null!", CommonConstants.ResponseNullErrorCode);
        }

        private static void SetResponseErrors<T>(IBaseResponse<T> response, string description, int code)
        {
            response.ErrorMessage = description;
            response.ErrorCode = code;
        }

        private static JsonSerializerSettings GenerateJsonSettings<T>(JsonConverter[] customConverters)
        {
            int length = customConverters != null ? customConverters.Length + 3 : 3;
            JsonConverter[] converters = new JsonConverter[length];

            if (customConverters != null && customConverters.Length > 0)
            {
                for (int i = 0; i < customConverters.Length; i++)
                {
                    converters[i] = customConverters[i];
                }
            }

            converters[length - 2] = new BaseResponseConverter<T>();

            JsonSerializerSettings jsonSettings = Configuration.JsonSettings;
            jsonSettings.Converters = converters;

            return jsonSettings;
        }
    }
}