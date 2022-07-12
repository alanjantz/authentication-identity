using Jantz.Authentication.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Jantz.Authentication.Services.Abstractions
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(string username, string password, string email, CancellationToken cancellationToken);
        Task<ServiceResponse<string>> Login(string username, string password, CancellationToken cancellationToken);
    }
}
