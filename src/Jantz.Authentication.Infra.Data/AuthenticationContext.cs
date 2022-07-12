using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jantz.Authentication.Infra.Data
{
    public class AuthenticationContext : IdentityDbContext<IdentityUser>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
