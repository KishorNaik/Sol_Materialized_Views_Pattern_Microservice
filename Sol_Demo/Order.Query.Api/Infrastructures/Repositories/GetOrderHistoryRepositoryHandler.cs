using Framework.SqlClient.Helper;
using MediatR;
using Order.Query.Api.Infrastructures.Abstracts;
using Order.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Order.Shared.DTO.Responses;

namespace Order.Query.Api.Infrastructures.Repositories
{
    public class GetOrderHistoryRepository : OrderHistoryRequestDTO, IRequest<IReadOnlyList<OrderHistoryResponseDTO>>
    {
    }

    public sealed class GetOrderHistoryRepositoryHandler : OrderRepositoryQueryAbstract, IRequestHandler<GetOrderHistoryRepository, IReadOnlyList<OrderHistoryResponseDTO>>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public GetOrderHistoryRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<IReadOnlyList<OrderHistoryResponseDTO>> IRequestHandler<GetOrderHistoryRepository, IReadOnlyList<OrderHistoryResponseDTO>>.Handle(GetOrderHistoryRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.GetParameterAsync("Get-Order-History", request);

                var result =
                       sqlClientDbProvider
                       ?.DapperBuilder
                       ?.OpenConnection(sqlClientDbProvider.GetConnection())
                       ?.Parameter(async () => await dynamicParameterTask)
                       ?.Command(async (dbConnection, dynamicParameter) =>
                       {
                           var resultSet =
                                (await
                                    dbConnection
                                    ?.QueryAsync<OrderHistoryResponseDTO, ProductResponseDTO, OrderHistoryResponseDTO>
                                    (
                                        sql: "uspGetOrders",
                                        param: dynamicParameter,
                                        map: (OrderHistoryResponse, ProductResponse) => new OrderHistoryResponseDTO()
                                        {
                                            OrderIdentity = OrderHistoryResponse.OrderIdentity,
                                            OrderDate = OrderHistoryResponse.OrderDate,
                                            SalesOrderNumber = OrderHistoryResponse.SalesOrderNumber,
                                            Quantity = OrderHistoryResponse.Quantity,
                                            ProductResponse = new ProductResponseDTO()
                                            {
                                                ProductIdentity = ProductResponse.ProductIdentity,
                                                ProductName = ProductResponse.ProductName,
                                                UnitPrice = ProductResponse.UnitPrice
                                            }
                                        },
                                        splitOn: "Split",
                                        commandType: CommandType.StoredProcedure
                                    )
                                )
                                ?.ToList()
                                ?.AsReadOnly();

                           return resultSet;
                       })
                       ?.ResultAsync<IReadOnlyList<OrderHistoryResponseDTO>>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}