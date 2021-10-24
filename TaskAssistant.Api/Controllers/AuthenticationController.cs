using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TaskAssistant.Api.Data;
using TaskAssistant.Api.Models.ClientModels;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Configuration;

namespace TaskAssistant.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticationController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, 
            IOptions<AuthOptions> authOptions,
            ICustomAuthenticationService authenticationService
            )
        {
            _context = context;
            _userManager = userManager;
            _authOptions = authOptions;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            if (loginModel is null)
            {
                throw new ArgumentNullException(nameof(loginModel));
            }

            if (await _authenticationService.ValidateUser(loginModel))
            {
                return new ObjectResult(await _authenticationService.BuildJwtToken(loginModel.UserName, tokenExpiryTime: 1));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}