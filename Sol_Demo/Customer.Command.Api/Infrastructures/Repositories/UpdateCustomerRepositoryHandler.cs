using Customer.Command.Api.Infrastructures.Abstracts;
using Customer.Command.Api.Infrastructures.ResultSets;
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
    public class UpdateCustomerRepository : CustomerRequestDTO, IRequest<UpdateCustomerResponseDTO>
    {
    }

    public sealed class UpdateCustomerRepositoryHandler : CustomerRepositoryCommandAbstract, IRequestHandler<UpdateCustomerRepository, UpdateCustomerResponseDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public UpdateCustomerRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<UpdateCustomerResponseDTO> IRequestHandler<UpdateCustomerRepository, UpdateCustomerResponseDTO>.Handle(UpdateCustomerRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dyanmicParameterTask = base.SetParameterAsync("Update-Customer", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dyanmicParameterTask)
                    ?.Command(async (dbConection, dyanmicParameter) =>
                    {
                        try
                        {
                            UpdateCustomerResponseDTO updateCustomerResponse =
                                 (await
                                 dbConection
                                 ?.QueryAsync<UpdateCustomerResultSet>(sql: "uspSetCustomer", param: dyanmicParameter, commandType: CommandType.StoredProcedure)
                                 )
                                 ?.Select((updateCustomerResultSet) => new UpdateCustomerResponseDTO()
                                 {
                                     UpdateNewCustomerResponse = new UpdateNewCustomerResponseDTO()
                                     {
                                         CustomerIdentity = updateCustomerResultSet.CustomerIdentity,
                                         FirstName = updateCustomerResultSet.FirstName,
                                         LastName = updateCustomerResultSet.LastName,
                                         MobileNo = updateCustomerResultSet.MobileNo
                                     },
                                     UpdateOldCustomerResponse = new UpdateOldCustomerResponseDTO()
                                     {
                                         FirstNameOldValue = updateCustomerResultSet.FirstNameOldValue,
                                         LastNameOldValue = updateCustomerResultSet.LastNameOldValue,
                                         MobileNoOldValue = updateCustomerResultSet.MobileNoOldValue
                                     }
                                 })
                                 ?.FirstOrDefault();

                            return updateCustomerResponse;
                        }
                        catch
                        {
                            throw;
                        }
                    })
                    ?.ResultAsync<UpdateCustomerResponseDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}