using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace RateMyP
    {
    public class SQLDbConnection : IDisposable
        {
        public SqlConnection Connection { get; }

        private bool m_isDisposed = false;

        public SQLDbConnection(string connectionString)
            {
            Connection = new SqlConnection(connectionString);
            }

        public static SQLDbConnection CreateToDb()
            {
            var connectionString = ConfigurationManager.AppSettings[RateMyPConfiguration.DbConnectionString];
            var dbConnection =  new SQLDbConnection(connectionString);
            dbConnection.Connection.Open();
            return dbConnection;
            }

        public void Dispose()
            {
            if (m_isDisposed) return;
            Connection.Close();
            Connection.Dispose();
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            }

        public object ExecuteCommand(string command)
            {
            using (var sqlCommand = new SqlCommand(command, Connection))
                {
                return sqlCommand.ExecuteScalar();
                }
            }

        public void ExecuteScript(string scriptName)
            {
            string script = "";
            if(scriptName.Equals("InitializeRateMyPCatalog.sql"))
                script = File.ReadAllText("D:\\Uni\\ratemyp\\RateMyP\\RateMyP\\Db\\Scripts\\InitializeRateMyPCatalog.sql"); // TODO: User resources to reach this SQL script

            if (string.IsNullOrEmpty(script))
                throw new ArgumentException($"script not found - {scriptName}");

            ExecuteCommand(script);
            }
        }
    }
