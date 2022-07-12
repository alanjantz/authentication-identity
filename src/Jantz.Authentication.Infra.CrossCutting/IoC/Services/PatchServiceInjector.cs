using Jantz.Authentication.Services.Common.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Jantz.Authentication.Infra.CrossCutting.IoC.Services
{
    public static class PatchServiceInjector
    {
        public static IHost ApplyPatchs(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var patchs = scope.ServiceProvider.GetServices<IPatch>();
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();

                if (configuration.GetValue<bool>("ApplyPatchs"))
                {
                    foreach (var patch in from patch in patchs
                                          where patch.CanApply()
                                          select patch)
                    {
                        patch.Apply();
                    }
                }
            }

            return host;
        }
    }
}
