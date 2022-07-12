using Jantz.Authentication.Domain.Enums;
using Jantz.Authentication.Services.Common.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Jantz.Authentication.Infra.Data.Patchs
{
    public class UserRolesPatch : IPatch
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string[] _roles = new string[] { UserRoles.Admin.ToString(), UserRoles.User.ToString() };


        public UserRolesPatch(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void Apply()
        {
            foreach (var role in _roles)
            {
                var roleExists = Services.Utils.AsyncHelper.RunSync(() => _roleManager.RoleExistsAsync(role));
                if (!roleExists)
                    Services.Utils.AsyncHelper.RunSync(() => _roleManager.CreateAsync(new IdentityRole(role)));
            }
        }

        public bool CanApply()
        {
            foreach (var role in _roles)
            {
                var roleExists = Services.Utils.AsyncHelper.RunSync(() => _roleManager.RoleExistsAsync(role));
                if (!roleExists)
                    return true;
            }

            return false;
        }
    }
}
