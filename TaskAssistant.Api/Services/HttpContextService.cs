using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskAssistant.Api.Services.Interfaces;

namespace TaskAssistant.Api.Services
{
    /// <summary>
    /// <inheritdoc cref="IHttpContextService"/>
    /// </summary>
    /// <seealso cref="IHttpContextService" />
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the user ip.
        /// </summary>
        /// <returns>
        /// User IP
        /// </returns>
        /// <value>
        /// The user ip.
        /// </value>
        public IPAddress UserIp => GetClientIP();

        /// <summary>
        /// Gets the user identifier from HTTP context.
        /// </summary>
        /// <returns>
        /// User Id
        /// </returns>
        /// <value>
        /// The user identifier from HTTP context.
        /// </value>
        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        /// <summary>
        /// Gets the route data.
        /// </summary>
        /// <value>
        /// The route data.
        /// </value>
        public RouteData RouteData => _httpContextAccessor.HttpContext.GetRouteData();

        /// <summary>
        /// Gets the Path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path => _httpContextAccessor.HttpContext.Request.Path;

        /// <summary>
        /// Gets the HTTP verb.
        /// </summary>
        /// <value>
        /// HTTP verb of the request
        /// </value>
        public string HttpVerb => _httpContextAccessor.HttpContext.Request.Method;

        /// <summary>
        /// Gets the device token from HTTP headers.
        /// </summary>
        /// <returns>
        /// Device Token
        /// </returns>
        /// <value>
        /// The device token from HTTP headers.
        /// </value>
        public string GetDeviceToken()
        {
            var deviceToken = _httpContextAccessor.HttpContext.Request.Headers["X-Device-Token"];
            return deviceToken.Any() ? deviceToken.ToString() : null;
        }

        /// <summary>
        /// Gets the X-Bypass-Login from HTTP headers.
        /// </summary>
        /// <returns>
        /// X-Bypass-Login
        /// </returns>
        /// <value>
        /// The X-Bypass-Login from HTTP headers.
        /// </value>
        public bool BypassLogin()
        {
            var bypassLogin = _httpContextAccessor.HttpContext.Request.Headers["X-Bypass-Login"];
            return bypassLogin.Any() ?
                bypassLogin.ToString().ToUpperInvariant().Equals("TRUE", System.StringComparison.OrdinalIgnoreCase) ? true : false :
                false;
        }

        /// <summary>
        /// Gets the Client Ip Address from HTTP headers.
        /// </summary>
        /// <returns>
        /// Client Ip Address
        /// </returns>
        /// <value>
        /// The Client Ip from HTTP headers.
        /// </value>
        private IPAddress GetClientIP()
        {
            string ipHeader = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            IPAddress clientIp;

            if (string.IsNullOrEmpty(ipHeader))
            {
                clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            }
            else
            {
                string clientIpAddress = ipHeader.Split(',')[0];
                _ = IPAddress.TryParse(clientIpAddress, out clientIp);
            }

            return clientIp;
        }
    }
}
