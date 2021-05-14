using Customer.Command.Api.Infrastructures.Abstracts;
using Framework.SqlClient.Helper;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Customer.Shared.DTO.Requests;

namespace Customer.Command.Api.Infrastructures.Repositories
{
    public class CreateCustomerRepository : CustomerRequestDTO, IRequest<ICreateCustomerResponseDTO>
    {
    }

    public sealed class CreateCustomerRepositoryHandler : CustomerRepositoryCommandAbstract, IRequestHandler<CreateCustomerRepository, ICreateCustomerResponseDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CreateCustomerRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<ICreateCustomerResponseDTO> IRequestHandler<CreateCustomerRepository, ICreateCustomerResponseDTO>.Handle(CreateCustomerRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Create-Customer", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            var customerResult =
                                await
                                dbConnection
                                .QueryFirstAsync<CreateCustomerResponseDTO>(sql: "uspSetCustomer", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return customerResult;
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    ?.ResultAsync<ICreateCustomerResponseDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}