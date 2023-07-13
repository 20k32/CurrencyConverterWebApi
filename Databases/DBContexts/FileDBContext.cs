using Databases.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Models;
using System.Globalization;


namespace Databases.DBContexts
{
    // логика может быть такой: раз в 30 минут отправлять запрос на сервер для обновления данных по валютам,
    // эти данные записывать в файл и потом использовать в течении 30 минут этот файл, потом данные перезаписываються
    // логику по кешированию данных можно определить в другом классе в слое Application т.к. это бизнес логика конкретно для веб приложения

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
