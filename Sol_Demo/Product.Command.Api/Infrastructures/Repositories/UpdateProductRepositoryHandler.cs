using Framework.SqlClient.Helper;
using MediatR;
using Product.Command.Api.Infrastructures.Abstracts;
using Product.Shared.DTO.Requests;
using Product.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Product.Command.Api.Infrastructures.ResultSets;

namespace Product.Command.Api.Infrastructures.Repositories
{
    public class UpdateProductRepository : ProductRequestDTO, IRequest<UpdateProductResponseDTO>
    {
    }

    public sealed class UpdateProductRepositoryHandler : ProductRepositoryCommandAbstract, IRequestHandler<UpdateProductRepository, UpdateProductResponseDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public UpdateProductRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<UpdateProductResponseDTO> IRequestHandler<UpdateProductRepository, UpdateProductResponseDTO>.Handle(UpdateProductRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Update-Product", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            UpdateProductResponseDTO updateProductResponseDTO =
                                (await
                                dbConnection
                                ?.QueryAsync<UpdateProductResultSet>(sql: "uspSetProduct", param: dynamicParameter, commandType: CommandType.StoredProcedure)
                                )?.Select((updateProductResultSet) => new UpdateProductResponseDTO()
                                {
                                    UpdateNewProductResponse = new UpdateNewProductResponseDTO()
                                    {
                                        ProductIdentity = updateProductResultSet.ProductIdentity,
                                        ProductName = updateProductResultSet.ProductName,
                                        UnitPrice = updateProductResultSet.UnitPrice
                                    },
                                    UpdateOldProductResponse = new UpdateOldProductResponseDTO()
                                    {
                                        ProductNameOldValue = updateProductResultSet.ProductNameOldValue,
                                        UnitPriceOldValue = updateProductResultSet.UnitPriceOldValue
                                    }
                                })
                                ?.FirstOrDefault();

                            return updateProductResponseDTO;
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    ?.ResultAsync<UpdateProductResponseDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}