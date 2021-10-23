using Ical.Net.DataTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskAssistant.Api.Data;
using TaskAssistant.Api.Models.ClientModels;
using TaskAssistant.Domain.Configuration;

namespace TaskAssistant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthenticationController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _userManager = userManager;
            _authOptions = authOptions;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            if (loginModel is null)
            {
                throw new ArgumentNullException(nameof(loginModel));
            }

            if (await ValidateUser(loginModel))
            {
                return new ObjectResult(await BuildJwtToken(loginModel.UserName, tokenExpiryTime: 1));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> ValidateUser(Login loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            return await _userManager.CheckPasswordAsync(user, loginModel.Password);
        }

        private async Task<dynamic> BuildJwtToken(string userName, int tokenExpiryTime)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == user.Id
                        select new { ur.UserId, ur.RoleId, r.Name };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(tokenExpiryTime)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var output = new
            {
                AccessToken = GenerateJWTToken(claims, DateTime.UtcNow.AddDays(tokenExpiryTime)),
                UserId = user.Id,
                UserName = userName,
                RolesIds = roles.ToDictionary(role=>role.RoleId, role=>role.Name)
            };
            return output;
        }

        private string GenerateJWTToken(IList<Claim> claims, DateTime tokenExpiryTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_authOptions.Value.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //var token = new JwtSecurityToken(_authOptions.Value.Issuer, null, claims, expires: tokenExpiryTime, signingCredentials: credentials);
            var token = new JwtSecurityToken(_authOptions.Value.Issuer, null, claims, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}