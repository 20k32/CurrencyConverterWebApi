using Databases;
using MediatR;
using Models;
using System.Runtime.CompilerServices;

namespace BaseActions.Queries.GetCurrencyLike
{
    public class GetCurrencyLikeQueryHandler : IRequestHandler<GetCurrencyLikeQuery, CurrencyListModel?>
    {
        private DBContext DbContext = null!;

        public GetCurrencyLikeQueryHandler(DBContext context) =>
            DbContext = context;

        public Task<CurrencyListModel?> Handle(GetCurrencyLikeQuery request, CancellationToken cancellationToken)
        {
            var result = DbContext.GetCurrencyLike(request.CurrencyName);

            return Task.FromResult(result)!;
        }
    }
}
