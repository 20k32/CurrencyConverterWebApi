using MediatR;
using Models;

namespace BaseActions.Queries.GetCurrencyLike
{
    public class GetCurrencyLikeQuery : IRequest<CurrencyListModel>
    {
        public string CurrencyName { get; set; }

        public GetCurrencyLikeQuery(string currencyName) =>
            CurrencyName = currencyName;
    }
}
