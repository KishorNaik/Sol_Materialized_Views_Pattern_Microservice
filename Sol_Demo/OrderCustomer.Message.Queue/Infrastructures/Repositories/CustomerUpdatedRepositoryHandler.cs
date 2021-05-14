using Dapper;
using Framework.SqlClient.Helper;
using MediatR;
using OrderCustomer.Message.Queue.Api.DTOs;
using OrderCustomer.Message.Queue.Infrastructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace OrderCustomer.Message.Queue.Infrastructures.Repositories
{
    public enum UpdateState
    {
        Customer_Name_Changed = 1,
        Customer_MobileNo_Changed = 2
    };

    public class CustomerUpdatedRepository : CustomerMessageDTO, INotification
    {
        public UpdateState UpdateState { get; set; }
    }

    public sealed class CustomerUpdatedRepositoryHandler : CustomerRepositoryMessageAbstract, INotificationHandler<CustomerUpdatedRepository>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CustomerUpdatedRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task INotificationHandler<CustomerUpdatedRepository>.Handle(CustomerUpdatedRepository notification, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterAsync = base.SetParameterAsync(notification.UpdateState.ToString(), notification);

                _ =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterAsync)
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