using Framework.SqlClient.Helper;
using MediatR;
using OrderProduct.Message.Queue.Infrastructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using OrderProduct.Message.Queue.DTOs;

namespace OrderProduct.Message.Queue.Infrastructures.Repositories
{
    public class ProductRemovedRepository : ProductMessageDTO, INotification
    {
    }

    public sealed class ProductRemovedRepositoryHandler : ProductRepositoryMessageAbstract, INotificationHandler<ProductRemovedRepository>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public ProductRemovedRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<ProductRemovedRepository>.Handle(ProductRemovedRepository notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Product-Removed", notification);

                _ =
                     sqlClientDbProvider
                     ?.DapperBuilder
                     ?.OpenConnection(sqlClientDbProvider.GetConnection())
                     ?.Parameter(async () => await dynamicParameterTask)
                     ?.Command(async (dbConnection, dynamicParameter) =>
                     {
                         try
                         {
                             int noOfRowAffected = await dbConnection?.ExecuteAsync(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure);
                             return (noOfRowAffected >= 1) ? true : false;
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