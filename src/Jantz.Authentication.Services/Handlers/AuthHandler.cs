using Jantz.Authentication.Services.Abstractions;
using Jantz.Authentication.Services.Commands.Auth;
using Jantz.Authentication.Services.Common.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jantz.Authentication.Services.Handlers
{
    public class AuthHandler : IRequestHandler<RegisterCommand, CommandResponse<string>>,
                               IRequestHandler<LoginCommand, CommandResponse<string>>
    {
        private readonly IAuthService _authService;

        public AuthHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CommandResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.Login(request.Username, request.Password, cancellationToken);

            if (result.IsValid)
                return CommandResponse.BuildResponse(result.Data);

            return CommandResponse.BuildInvalidResponse(result.Data, result.Errors);
        }

        public async Task<CommandResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(request.Username, request.Password, request.Email, cancellationToken);

            if (result.IsValid)
                return CommandResponse.BuildResponse(result.Data);

            return CommandResponse.BuildInvalidResponse(result.Data, result.Errors);
        }
    }
}
