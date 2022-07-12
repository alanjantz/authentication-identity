using Jantz.Authentication.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jantz.Authentication.Infra.CrossCutting.IoC.Services
{
    public static class DataBaseServiceInjector
    {
        public static void AddDbServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseNpgsql(connectionString, op => op.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)), ServiceLifetime.Transient);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>()
                .AddDefaultTokenProviders();

            services.AddHealthChecks()
                .AddDbContextCheck<DbContext>();
        }

        public static void UseDbServices(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<AuthenticationContext>();

            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }
        }
    }
}
