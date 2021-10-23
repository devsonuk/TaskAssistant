using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAssistant.Domain.Configuration;

namespace TaskAssistant.Api.Extensions.ServiceCollection
{
    /// <summary>
    /// Custom Options Extension
    /// </summary>
    public static class CustomOptionsExtension
    {
        /// <summary>
        /// Configures the custom options.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configSection">The configuration section.</param>
        public static void ConfigureCustomOptions(this IServiceCollection services, IConfigurationSection configSection)
        {
            // configure AuthOptions, contains Options related to JWT Authentication
            services.Configure<AuthOptions>(configSection.GetSection("AuthOptions"));
        }
    }
}
