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
    public class ProductUnitPriceChangedReposiotry : ProductMessageDTO, INotification
    {
    }

    public sealed class ProductUnitPriceChangedReposiotryHandler : ProductRepositoryMessageAbstract, INotificationHandler<ProductUnitPriceChangedReposiotry>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public ProductUnitPriceChangedReposiotryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<ProductUnitPriceChangedReposiotry>.Handle(ProductUnitPriceChangedReposiotry notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Product-UnitPrice-Changed", notification);

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