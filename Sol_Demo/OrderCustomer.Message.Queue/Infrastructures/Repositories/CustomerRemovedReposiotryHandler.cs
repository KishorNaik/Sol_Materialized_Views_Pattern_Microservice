using Dapper;
using Framework.SqlClient.Helper;
using MediatR;
using OrderCustomer.Message.Queue.Api.DTOs;
using OrderCustomer.Message.Queue.Infrastructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderCustomer.Message.Queue.Infrastructures.Repositories
{
    public class CustomerRemovedRepository : CustomerMessageDTO, INotification
    {
    }

    public sealed class CustomerRemovedReposiotryHandler : CustomerRepositoryMessageAbstract, INotificationHandler<CustomerRemovedRepository>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CustomerRemovedReposiotryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<CustomerRemovedRepository>.Handle(CustomerRemovedRepository notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Customer-Removed", notification);

                _ =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            _ =
                                   await
                                   dbConnection
                                   .ExecuteAsync(sql: "uspSetCustomer", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return true;
                        }
                        catch
                        {
                            throw;
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