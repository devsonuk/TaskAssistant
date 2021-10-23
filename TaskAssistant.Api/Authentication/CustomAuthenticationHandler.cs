using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Configuration;

namespace TaskAssistant.Api.Authentication
{
    /// <summary>
    /// CustomAuthenticationHandler
    /// </summary>
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IHttpContextService _httpContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthenticationHandler"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">logger</param>
        /// <param name="encoder">encoder</param>
        /// <param name="clock">clock</param>
        /// <param name="authOptions">appsettigns</param>
        /// <param name="httpContextService">httpContextService</param>
        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<AuthOptions> authOptions,
            IHttpContextService httpContextService)
            : base(options, logger, encoder, clock)
        {
            _authOptions = authOptions;
            _httpContextService = httpContextService;
        }

        /// <summary>
        /// HandleAuthenticateAsync
        /// </summary>
        /// <returns>Task</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (Request.Headers.ContainsKey("X-Bypass-Login") &&
                _httpContextService.BypassLogin())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Authentication, "TRUE")
                };
                var identity = new ClaimsIdentity(claims, "Bypass login claim");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            try
            {
                var authorizationHeaderToken = Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorizationHeaderToken))
                {
                    return AuthenticateResult.NoResult();
                }

                return ValidateToken(authorizationHeaderToken);
            }
            catch (SecurityTokenExpiredException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            catch (AuthenticationException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private AuthenticateResult ValidateToken(string authorizationHeaderToken)
        {
            var token = authorizationHeaderToken.Substring("bearer".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _authOptions.Value.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_authOptions.Value.SecurityKey))
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            if (validatedToken == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}