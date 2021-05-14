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
    public class CustomerCreatedRepository : CustomerMessageDTO, INotification
    {
    }

    public class CustomerCreatedRepositoryHandler : CustomerRepositoryMessageAbstract, INotificationHandler<CustomerCreatedRepository>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CustomerCreatedRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<CustomerCreatedRepository>.Handle(CustomerCreatedRepository notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Customer-Created", notification);

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