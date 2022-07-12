using Jantz.Authentication.Services.Common.Commands;
using MediatR;

namespace Jantz.Authentication.Services.Common.Abstractions
{
    public interface ICommand<T> : IRequest<CommandResponse<T>>
    {
    }

    public interface ICommand : IRequest<CommandResponse>
    {
    }
}
