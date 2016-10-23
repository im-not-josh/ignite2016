namespace Xtrade.Shared.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Repository;
    using SQLite;
    using Xamarin.Forms;

    public class XtradeRepository : IXtradeRepository
    {
        private readonly SQLiteAsyncConnection _databaseConnection;

        public XtradeRepository()
        {
            this._databaseConnection = new SQLiteAsyncConnection(DatabaseFilePath, true);
            this._databaseConnection.CreateTableAsync<Rate>().Wait();
        }

        private static string DatabaseFilePath
        {
            get
            {
                const string sqliteFilename = "XtradeDatabase.db3";
                string path = string.Empty;
                string libraryPath;
                if (Device.OS == TargetPlatform.Android)
                {
                    libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                else
                {
                    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    libraryPath = System.IO.Path.Combine(documentsPath, "..", "Library", "Databases");

                    if (!System.IO.Directory.Exists(libraryPath))
                    {
                        System.IO.Directory.CreateDirectory(libraryPath);
                    }
                }

                path = System.IO.Path.Combine(libraryPath, sqliteFilename);
                return path;
            }
        }

        public async Task InsertRatesAsync(IList<IRate> newRates)
        {
            await this._databaseConnection.RunInTransactionAsync(transaction =>
            {
                if (newRates != null && newRates.Count > 0)
                {
                    foreach (IRate newRate in newRates)
                    {
                        newRate.DeviceModified = DateTime.UtcNow;
                        transaction.InsertOrReplace(newRate);
                    }
                }
            });
        }

        public async Task InsertRateAsync(IRate newRate)
        {
            await this._databaseConnection.UpdateAsync(newRate);
        }

        public async Task<IList<Rate>> GetAllRates()
        {
            return await this._databaseConnection.Table<Rate>().ToListAsync();
        }

        public async Task<Rate> GetRateByCode(string code)
        {
            return await this._databaseConnection.Table<Rate>().Where(i => i.CurrencyCode == code).FirstOrDefaultAsync();
        }
    }
}
