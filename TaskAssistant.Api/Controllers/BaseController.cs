using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssistant.Api.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represents the base controller class
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    //[Authorize(Policy = "Authenticated")]
    //[Authorize(Policy = "UserAccessPolicy")]
    public class BaseController : ControllerBase
    {

    }
}
