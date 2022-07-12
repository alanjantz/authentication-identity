using Jantz.Authentication.Services.Common.Abstractions;

namespace Jantz.Authentication.Services.Commands.Auth
{
    public class LoginCommand : ICommand<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
