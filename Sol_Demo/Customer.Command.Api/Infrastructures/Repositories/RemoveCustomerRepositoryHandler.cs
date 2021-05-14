using Customer.Command.Api.Infrastructures.Abstracts;
using Customer.Shared.DTO.Requests;
using Customer.Shared.DTO.Responses;
using Framework.SqlClient.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Customer.Command.Api.Infrastructures.Repositories
{
    public sealed class RemoveCustomerRepository : CustomerRequestDTO, IRequest<bool>
    {
    }

    public sealed class RemoveCustomerRepositoryHandler : CustomerRepositoryCommandAbstract, IRequestHandler<RemoveCustomerRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public RemoveCustomerRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<RemoveCustomerRepository, bool>.Handle(RemoveCustomerRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Remove-Customer", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            int rowAfected =
                             await
                             dbConnection
                                 ?.ExecuteAsync(sql: "uspSetCustomer", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return (rowAfected >= 1) ? true : false;
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