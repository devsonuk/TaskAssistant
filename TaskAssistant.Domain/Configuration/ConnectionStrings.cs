using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssistant.Domain.Configuration
{
    /// <summary>
    /// Represents DB connection string level settings
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// Gets or sets DB connection string from the config
        /// </summary>
        /// <value>
        /// DB connection string from the config
        /// </value>
        public string TaskAssistant { get; set; }

        /// <summary>
        /// Gets or sets eds DB connection string from the config
        /// </summary>
        /// <value>
        /// Eds DB connection string from the config
        /// </value>
        public string TaskAssistantAuth { get; set; }
    }
}
