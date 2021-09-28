using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.DAL
{
    public class Configuration
    {
        private const string connectionStringName = "TradingToolsConnectionString";
        public static void SetConnectionString(string connectionString)
        {
            ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
        }

        public static string GetConnectionString()
        {
           
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}
