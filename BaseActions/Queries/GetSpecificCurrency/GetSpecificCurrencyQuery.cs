using MediatR;
using Models;

namespace BaseActions.Queries.GetSpecificCurrency
{
    public class GetSpecificCurrencyQuery : IRequest<CurrencyListItemModel?>
    {
        public string CurrencyName { get; set; } = null!;

        public GetSpecificCurrencyQuery(string currencyName) =>
            CurrencyName = currencyName;
    }
}
