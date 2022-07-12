using Jantz.Authentication.Domain.ApiResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Jantz.Authentication.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerResponse(400, type: typeof(HttpErrorResponse))]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(500, type: typeof(HttpErrorResponse))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApiController : ControllerBase
    {
        public const string BASE_PATH = "api/[controller]/[action]";

        [NonAction]
        public ActionResult<HttpResponse> ApiResponse(HttpStatusCode httpCode, object data = null, IEnumerable<string> errors = null)
        {
            HttpResponse response = CreateResponse(httpCode, data, errors);

            switch (httpCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return Ok(response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);
                case HttpStatusCode.NoContent:
                default:
                    return NoContent();
            }
        }

        private HttpResponse CreateResponse(HttpStatusCode httpCode, object data = null, IEnumerable<string> errors = null)
        {
            switch (httpCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return data != null ? new HttpContentResponse<object>(httpCode, data) : new HttpResponse(httpCode);
                case HttpStatusCode.NotFound:
                case HttpStatusCode.BadRequest:
                default:
                    return new HttpErrorResponse(httpCode, errors);
            }
        }
    }
}
