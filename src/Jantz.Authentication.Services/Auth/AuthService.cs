using Jantz.Authentication.Domain.Enums;
using Jantz.Authentication.Domain.Models;
using Jantz.Authentication.Domain.Models.Settings;
using Jantz.Authentication.Domain.Resources;
using Jantz.Authentication.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jantz.Authentication.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password, CancellationToken cancellationToken)
        {
            var result = new ServiceResponse<string>();

            var user = await _userManager.FindByNameAsync(username);
            if (user is not null && await _userManager.CheckPasswordAsync(user, password))
            {
                var authClaims = new Dictionary<string, object>
                {
                    [ClaimTypes.Name] = user.UserName,
                    [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                    authClaims[ClaimTypes.Role] = userRole;

                result.SetData(GenerateToken(username, authClaims));
            }
            else
                result.AddNotification(Messages.InvalidUserOrPassword);

            return result;
        }

        public async Task<ServiceResponse<string>> Register(string username, string password, string email, CancellationToken cancellationToken)
        {
            var result = new ServiceResponse<string>();

            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser is not null)
                result.AddNotification(string.Format(Messages.InvalidUserOrPassword, username));
            else
            {
                IdentityUser user = new()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = username
                };

                var identityResult = await _userManager.CreateAsync(user, password);
                if (identityResult.Succeeded)
                {
                    result.SetData(GenerateToken(username));

                    await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
                }
                else
                    result.AddNotification(identityResult.Errors.Select(x => x.Description));
            }

            return result;
        }

        private string GenerateToken(string username, IDictionary<string, object> authClaims = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            DateTime creationDate = DateTime.Now,
                     expirationDate = creationDate + TimeSpan.FromSeconds(_jwtSettings.ExpirationMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, username)
                }),

                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = creationDate,
                Expires = expirationDate,
                Claims = authClaims
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
