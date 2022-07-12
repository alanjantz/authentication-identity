using Jantz.Authentication.Services.Common.Abstractions;

namespace Jantz.Authentication.Services.Commands.Auth
{
    public class RegisterCommand : ICommand<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
