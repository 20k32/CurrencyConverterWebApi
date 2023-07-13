using Models;

namespace Databases
{
    public abstract class DBContext
    {
        public abstract CurrencyListModel GetAllCurrencies();
        public abstract CurrencyListModel GetCurrencyLike(string id);
        public abstract CurrencyListItemModel? GetSpecificCurrency(string id);
        public abstract void SetData(ApiRequestModel? data);

        protected const string DATABASE_NAME = "rates";
    }
}
