using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssistant.Domain.Configuration
{
    /// <summary>
    /// Authentication related Options defined in <see cref="AppSettings"/>
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Gets or sets the security key.
        /// </summary>
        /// <value>
        /// The security key.
        /// </value>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the maximum login attempts.
        /// </summary>
        /// <value>
        /// The maximum login attempts.
        /// </value>
        public int MaxLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the login lock down minutes.
        /// </summary>
        /// <value>
        /// The login lock down minutes.
        /// </value>
        public int LoginLockDownMinutes { get; set; }
    }
}
