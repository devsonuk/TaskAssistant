using Microsoft.AspNetCore.Routing;
using System.Net;

namespace TaskAssistant.Api.Services.Interfaces
{
    public interface IHttpContextService
    {
        /// <summary>
        /// Gets the user ip.
        /// </summary>
        /// <returns>User IP</returns>
        /// <value>
        /// The user ip.
        /// </value>
        IPAddress UserIp { get; }

        /// <summary>
        /// Gets the user identifier from HTTP context.
        /// </summary>
        /// <returns>User Id</returns>
        /// <value>
        /// The user identifier from HTTP context.
        /// </value>
        string UserId { get; }

        /// <summary>
        /// Gets the route data.
        /// </summary>
        /// <value>
        /// The route data.
        /// </value>
        RouteData RouteData { get; }

        /// <summary>
        /// Gets the Path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets the HTTP verb.
        /// </summary>
        /// <returns>
        /// HTTP verb
        /// </returns>
        /// <value>
        /// HTTP verb of the request
        /// </value>
        string HttpVerb { get; }

        /// <summary>
        /// Gets the device token from HTTP headers.
        /// </summary>
        /// <returns>Device Token</returns>
        /// <value>
        /// The device token from HTTP headers.
        /// </value>
        string GetDeviceToken();

        /// <summary>
        /// Gets the X-Bypass-Login from HTTP headers.
        /// </summary>
        /// <returns>
        /// X-Bypass-Login
        /// </returns>
        /// <value>
        /// The X-Bypass-Login from HTTP headers.
        /// </value>
        bool BypassLogin();
    }
}
