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
    public class ProductCreatedRepository : ProductMessageDTO, INotification
    {
    }

    public class ProductCreatedRepositoryHandler : ProductRepositoryMessageAbstract, INotificationHandler<ProductCreatedRepository>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public ProductCreatedRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<ProductCreatedRepository>.Handle(ProductCreatedRepository notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Product-Created", notification);

                _ =
                     sqlClientDbProvider
                     ?.DapperBuilder
                     ?.OpenConnection(sqlClientDbProvider.GetConnection())
                     ?.Parameter(async () => await dynamicParameterTask)
                     ?.Command(async (dbConnection, dynamicParameter) =>
                     {
                         try
                         {
                             await dbConnection?.ExecuteAsync(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure);
                             return true;
                         }
                         catch
                         {
                             return null;
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