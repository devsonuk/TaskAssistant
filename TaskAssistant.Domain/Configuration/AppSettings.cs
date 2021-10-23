using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssistant.Domain.Configuration
{
    /// <summary>
    /// Represents app config settings model
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets DB connection string
        /// </summary>
        /// <value>
        /// DB connection string
        /// </value>
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
