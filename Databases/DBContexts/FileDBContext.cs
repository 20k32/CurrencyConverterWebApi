using Databases.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Models;
using System.Globalization;


namespace Databases.DBContexts
{
    public class FileDBContext : DBContext
    {
        private ApiRequestModel Data = null!;

        public FileDBContext()
        { }

        public override CurrencyListModel GetAllCurrencies() =>
            Data.Rates
            .Select(kv => new CurrencyListItemModel(kv.Key, kv.Value))
            .ToList();

        public override CurrencyListModel GetCurrencyLike(string name) =>
            Data.Rates
                .Where(currency => currency.Key.ToLower().Contains(name.ToLower()))
                .Select(kv => new CurrencyListItemModel(kv.Key, kv.Value))
                .ToList();

        public override CurrencyListItemModel? GetSpecificCurrency(string name)
        {
            if (Data.Rates.TryGetValue(name, out var currency))
            {
                return new CurrencyListItemModel(name, currency);
            }

            return null;
        }

        public override void SetData(ApiRequestModel? data)
        {
            if(data == null)
            {
                return;
            }

            Data = data;
        }
    }
}
