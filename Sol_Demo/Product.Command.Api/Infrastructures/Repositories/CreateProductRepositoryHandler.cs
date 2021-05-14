using Framework.SqlClient.Helper;
using MediatR;
using Product.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Product.Command.Api.Infrastructures.Abstracts;
using Product.Shared.DTO.Requests;
using Product.Shared.DTO.Responses;

namespace Product.Command.Api.Infrastructures.Repositories
{
    public class CreateProductRepository : ProductRequestDTO, IRequest<ICreateProductResponseDTO>
    {
    }

    public sealed class CreateProductRepositoryHandler : ProductRepositoryCommandAbstract, IRequestHandler<CreateProductRepository, ICreateProductResponseDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CreateProductRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<ICreateProductResponseDTO> IRequestHandler<CreateProductRepository, ICreateProductResponseDTO>.Handle(CreateProductRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Create-Product", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            CreateProductResponseDTO createProductResponseDTO = await dbConnection?.QueryFirstOrDefaultAsync<CreateProductResponseDTO>(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure);
                            return createProductResponseDTO;
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    ?.ResultAsync<ICreateProductResponseDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}