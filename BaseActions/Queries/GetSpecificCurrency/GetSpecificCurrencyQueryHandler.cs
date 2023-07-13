using Databases;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseActions.Queries.GetSpecificCurrency
{
    internal class GetSpecificCurrencyQueryHandler : IRequestHandler<GetSpecificCurrencyQuery, CurrencyListItemModel?>
    {
        private DBContext DbContext = null!;

        public GetSpecificCurrencyQueryHandler(DBContext context) =>
            DbContext = context;

        public Task<CurrencyListItemModel?> Handle(GetSpecificCurrencyQuery request, CancellationToken _)
        {
            var result = DbContext.GetSpecificCurrency(request.CurrencyName);

            return Task.FromResult(result);
        }
    }
}
