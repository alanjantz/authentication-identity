using Jantz.Authentication.Infra.CrossCutting.IoC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Jantz.Authentication.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .ApplyPatchs()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
