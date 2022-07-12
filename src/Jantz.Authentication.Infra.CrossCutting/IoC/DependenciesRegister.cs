using Jantz.Authentication.Infra.CrossCutting.IoC.Services;
using Jantz.Authentication.Infra.Data.Patchs;
using Jantz.Authentication.Services.Abstractions;
using Jantz.Authentication.Services.Auth;
using Jantz.Authentication.Services.Common.Abstractions;
using Jantz.Authentication.Services.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jantz.Authentication.Infra.CrossCutting.IoC
{
    public static class DependenciesRegister
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbServices(configuration.GetConnectionString("DatabaseConnection"));
            services.AddAuthentication(configuration);
            services.AddBearerAuthorization();
            services.AddScoped<AuthHandler>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddPatches();
        }

        public static void AddPatches(this IServiceCollection services)
        {
            services.AddScoped<IPatch, UserRolesPatch>();
        }
    }
}