using Framework.SqlClient.Helper;
using MediatR;
using Product.Command.Api.Infrastructures.Abstracts;
using Product.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Product.Command.Api.Infrastructures.Repositories
{
    public class RemoveProductRepository : ProductRequestDTO, IRequest<bool>
    {
    }

    public sealed class RemoveProductRepositoryHandler : ProductRepositoryCommandAbstract, IRequestHandler<RemoveProductRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public RemoveProductRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<RemoveProductRepository, bool>.Handle(RemoveProductRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Remove-Product", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbOcnnection, dynamicParameter) =>
                    {
                        try
                        {
                            int noOfRowsAffected =
                                await
                                dbOcnnection
                                ?.ExecuteAsync(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return (noOfRowsAffected >= 1) ? true : false;
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