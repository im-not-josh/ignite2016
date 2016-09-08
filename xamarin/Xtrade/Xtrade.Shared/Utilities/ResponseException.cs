namespace Xtrade.Shared.Utilities
{
    using System;

    public class ResponseException : Exception
    {
        public ResponseException(string message, int code) : base(message)
        {
            this.Code = code;
        }

        public int Code { get; set; }
    }
}