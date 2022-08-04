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

        private const string sqlServerConnectionString = @"Server=localhost\SQLEXPRESS;Database=TradingToolsProd;Trusted_Connection=True;";
        private const string sqliteConnectionString = @"Data source=TradingTools.db";

        public static void SetConnectionString(string connectionString)
        {
            ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
        }

        public static string GetConnectionString(DatabaseProvider provider)
        {
            switch (provider)
            {
                case DatabaseProvider.SqlServer: return sqlServerConnectionString;
                case DatabaseProvider.Sqlite: return sqliteConnectionString;
                default: return sqlServerConnectionString;
            };
        }

        public enum DatabaseProvider
        {
            SqlServer,
            Sqlite
        }
    }
}
