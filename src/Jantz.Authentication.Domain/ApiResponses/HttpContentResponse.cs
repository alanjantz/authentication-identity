using System.Net;

namespace Jantz.Authentication.Domain.ApiResponses
{
    public class HttpContentResponse<T> : HttpResponse
    {
        public T Data { get; private set; }

        public HttpContentResponse(HttpStatusCode httpCode, T data) : base(httpCode)
        {
            Data = data;
        }
    }
}
