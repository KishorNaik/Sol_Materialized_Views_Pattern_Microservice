using DapperFluent;
using DapperFluent.Configuration;
using DapperFluent.Helpers;
using Framework.SqlClient.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SqlClient.Extensions
{
    public static class SqlProviderExtension
    {
        public static void AddSqlProvider(this IServiceCollection services, string connectionString)
        {
            services.AddDapperFluent();

            services.AddTransient<ISqlClientDbProvider, SqlClientDbProvider>((config) =>
            {
                IDapperBuilder dapperBuilder = config.GetRequiredService<IDapperBuilder>();

                return new SqlClientDbProvider(dapperBuilder, connectionString);
            });
        }
    }
}