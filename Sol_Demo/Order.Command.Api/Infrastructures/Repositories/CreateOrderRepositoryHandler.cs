using Framework.SqlClient.Helper;
using MediatR;
using Order.Command.Api.Infrastructures.Abstracts;
using Order.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Order.Command.Api.Infrastructures.Repositories
{
    public class CreateOrderRepository : OrderRequestDTO, IRequest<bool>
    {
    }

    public sealed class CreateOrderRepositoryHandler : OrderRepositoryCommandAbstract, IRequestHandler<CreateOrderRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CreateOrderRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<CreateOrderRepository, bool>.Handle(CreateOrderRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Create-Order", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider?.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            var noOfRowAffected =
                                await
                                dbConnection
                                ?.ExecuteAsync(sql: "uspSetOrder", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return (noOfRowAffected >= 1) ? true : false;
                        }
                        catch
                        {
                            return false;
                        }
                    })
                    ?.ResultAsync<bool>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}