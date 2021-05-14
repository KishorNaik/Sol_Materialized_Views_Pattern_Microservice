using Framework.SqlClient.Helper;
using MediatR;
using OrderProduct.Message.Queue.DTOs;
using OrderProduct.Message.Queue.Infrastructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace OrderProduct.Message.Queue.Infrastructures.Repositories
{
    public class ProductNameChangedReposiotry : ProductMessageDTO, INotification
    {
    }

    public sealed class ProductNameChangedReposiotryHandler : ProductRepositoryMessageAbstract, INotificationHandler<ProductNameChangedReposiotry>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public ProductNameChangedReposiotryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<ProductNameChangedReposiotry>.Handle(ProductNameChangedReposiotry notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Product-Name-Changed", notification);

                _ =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            int noOfRowsAffected =
                                await
                                dbConnection
                                ?.ExecuteAsync(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return (noOfRowsAffected >= 1) ? true : false;
                        }
                        catch
                        {
                            return false;
                        }
                    })
                    ?.ResultAsync<bool>();

                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
    }
}