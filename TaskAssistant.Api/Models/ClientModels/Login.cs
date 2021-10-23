using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssistant.Api.Models.ClientModels
{
    /// <summary>
    /// Represents the Login Client Request Model
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
