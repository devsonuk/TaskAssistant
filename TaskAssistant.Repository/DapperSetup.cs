using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DapperExtensions.Mapper;

namespace TaskAssistant.Repository
{
    /// <summary>
    /// A static class that provides a method to configure Dapper settings
    /// </summary>
    public static class DapperSetup
    {
        /// <summary>
        /// Method to set default Dapper settings
        /// </summary>
        public static void SetUpDapperExtensions()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);

            // Tell Dapper Extension to scan this assembly for custom mappings
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                Assembly.Load("TaskAssistant.Repository")
            });
        }
    }
}
