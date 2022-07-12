using System.Collections.Generic;
using System.Net;

namespace Jantz.Authentication.Domain.ApiResponses
{
    public class HttpErrorResponse : HttpResponse
    {
        public IEnumerable<string> Errors { get; private set; }

        public HttpErrorResponse(HttpStatusCode httpCode, IEnumerable<string> errors) : base(httpCode)
        {
            Success = false;
            Errors = errors;
        }
    }
}
