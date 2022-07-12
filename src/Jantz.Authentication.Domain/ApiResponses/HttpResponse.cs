using System.Net;

namespace Jantz.Authentication.Domain.ApiResponses
{
    public class HttpResponse
    {
        public bool Success { get; protected set; }
        public int HttpCode { get; protected set; }

        public HttpResponse(HttpStatusCode httpCode)
        {
            Success = true;
            HttpCode = (int)httpCode;
        }
    }
}
