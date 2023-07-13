using Databases;
using MediatR;
using Models;

namespace BaseActions.Queries.GetCurrencyList
{
    public class GetCurrencyListQueryHandler : IRequestHandler<GetCurrencyListQuery, CurrencyListModel>
    {
        private DBContext DbContext = null!;

        public GetCurrencyListQueryHandler(DBContext context) =>
            DbContext = context;

        public Task<CurrencyListModel> Handle(GetCurrencyListQuery request, CancellationToken _)
        {
            var result = DbContext.GetAllCurrencies();
            return Task.FromResult(result);
        }
    }
}
