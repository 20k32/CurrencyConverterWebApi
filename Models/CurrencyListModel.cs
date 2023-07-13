using System.Runtime.CompilerServices;

namespace Models
{
    public class CurrencyListModel
    {
        public IList<CurrencyListItemModel> CurrencyList = null!;

        public CurrencyListModel()
        { }

        public CurrencyListModel(IList<CurrencyListItemModel> currencyList) =>
            CurrencyList = currencyList;

        public static implicit operator CurrencyListModel(List<CurrencyListItemModel> list)
            => new CurrencyListModel(list);

        public IEnumerable<CurrencyListItemModel> GetEnumerator()
            => CurrencyList;
    }
}
