using MediatR;
using Models;

namespace BaseActions.Queries.GetCurrencyList
{
    public class GetCurrencyListQuery : IRequest<CurrencyListModel>
    { }
}
