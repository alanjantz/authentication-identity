using Jantz.Authentication.Domain.Common;
using System.Collections.Generic;

namespace Jantz.Authentication.Services.Common.Commands
{
    public class CommandResponse
    {
        public readonly DomainNotification DomainNotification;

        public bool IsValid { get; private set; }

        protected CommandResponse(bool isValid = true)
        {
            IsValid = isValid;
        }

        public CommandResponse(DomainNotification notification) : this(notification.IsValid)
        {
            DomainNotification = notification;
        }

        public static CommandResponse BuildResponse(DomainNotification notification)
            => new(notification);

        public static CommandResponse<T> BuildResponse<T>(DomainNotification notification, T response)
            => new(notification, response);

        public static CommandResponse<T> BuildResponse<T>(T response, bool isValid = true)
            => new(isValid, response);

        public static CommandResponse<T> BuildInvalidResponse<T>(T response, string error)
            => BuildInvalidResponse(response, new string[] { error });

        public static CommandResponse<T> BuildInvalidResponse<T>(T response, IEnumerable<string> errors)
        {
            var notification = new DomainNotification();
            notification.AddNotification(errors);
            return new(notification, response);
        }
    }

    public class CommandResponse<T> : CommandResponse
    {
        public T Response { get; private set; }

        internal CommandResponse(bool isValid, T response) : base(isValid)
        {
            Response = response;
        }

        internal CommandResponse(DomainNotification notification, T response) : base(notification)
        {
            Response = response;
        }
    }
}
