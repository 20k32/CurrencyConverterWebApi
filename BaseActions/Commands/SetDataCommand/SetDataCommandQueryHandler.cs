using Databases;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseActions.Commands.SetDataCommand
{
    public class SetDataCommandQueryHandler : IRequestHandler<SetDataCommandQuery, Unit>
    {
        private DBContext DbContext = null!;

        public SetDataCommandQueryHandler(DBContext context)
        {
            DbContext = context;
        }

        public Task<Unit> Handle(SetDataCommandQuery request, CancellationToken cancellationToken)
        {
            DbContext.SetData(request.DataToSet);

            return Task.FromResult(Unit.Value);
        }
    }
}
