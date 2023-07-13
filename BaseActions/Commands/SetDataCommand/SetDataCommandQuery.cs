using MediatR;
using Models;

namespace BaseActions.Commands.SetDataCommand
{
    public class SetDataCommandQuery : IRequest<Unit>
    {
        public ApiRequestModel DataToSet = null!;

        public SetDataCommandQuery(ApiRequestModel dataToSet) =>
            DataToSet = dataToSet;
    }
}
