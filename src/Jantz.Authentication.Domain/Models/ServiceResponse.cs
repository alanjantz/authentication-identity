using Jantz.Authentication.Domain.Common;

namespace Jantz.Authentication.Domain.Models
{
    public class ServiceResponse<T> : DomainNotification
    {
        public T Data { get; private set; }

        public void SetData(T data)
        {
            Data = data;
        }
    }
}
