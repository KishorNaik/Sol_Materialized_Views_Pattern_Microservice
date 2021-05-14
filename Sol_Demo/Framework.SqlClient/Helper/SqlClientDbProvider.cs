using DapperFluent.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Framework.SqlClient.Helper
{
    public interface ISqlClientDbProvider : IDbProviders<SqlConnection>
    {
    }

    public sealed class SqlClientDbProvider : ISqlClientDbProvider
    {
        private IDapperBuilder dapperBuilder = null;
        private String connectionString = null;

        public SqlClientDbProvider(IDapperBuilder dapperBuilder, String connectionString)
        {
            this.dapperBuilder = dapperBuilder;
            this.connectionString = connectionString;
        }

        IDapperBuilder IDbProviders<SqlConnection>.DapperBuilder => this.dapperBuilder;

        SqlConnection IDbProviders<SqlConnection>.GetConnection()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}