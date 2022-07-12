using Jantz.Authentication.Domain.ApiResponses;
using Jantz.Authentication.Services.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Jantz.Authentication.Api.Controllers
{
    [Route(BASE_PATH)]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<HttpResponse>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsValid)
                return ApiResponse(HttpStatusCode.OK, result.Response);

            return ApiResponse(HttpStatusCode.Unauthorized, result.DomainNotification.Errors);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<HttpResponse>> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsValid)
                return ApiResponse(HttpStatusCode.OK);

            return ApiResponse(HttpStatusCode.BadRequest, result.DomainNotification.Errors);
        }

        [HttpGet]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);
    }
}
