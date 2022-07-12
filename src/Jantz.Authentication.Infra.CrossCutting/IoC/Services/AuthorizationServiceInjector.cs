using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jantz.Authentication.Infra.CrossCutting.IoC.Services
{
    public static class AuthorizationServiceInjector
    {
        public static void AddBearerAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void UseAuthorizationServices(this IApplicationBuilder app)
        {
            app.UseAuthorization();
        }
    }
}
