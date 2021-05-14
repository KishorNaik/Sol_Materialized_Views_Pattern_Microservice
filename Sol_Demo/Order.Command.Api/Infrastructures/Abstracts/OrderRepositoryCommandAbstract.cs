using Dapper;
using Order.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Order.Command.Api.Infrastructures.Abstracts
{
    public abstract class OrderRepositoryCommandAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, IOrderRequestDTO orderRequest)
        {
            try
            {
                return Task.Run(() =>
                {
                    DynamicParameters dynamicParameters = new();

                    dynamicParameters.Add("@Command", command, DbType.String, direction: ParameterDirection.Input);
                    dynamicParameters.Add("@OrderIdentity", orderRequest.OrderIdentity, DbType.Guid, direction: ParameterDirection.Input);
                    dynamicParameters.Add("@CustomerIdentity", orderRequest.CustomerIdentity, DbType.Guid, direction: ParameterDirection.Input);
                    dynamicParameters.Add("@ProductIdentity", orderRequest.ProductIdentity, DbType.Guid, direction: ParameterDirection.Input);
                    dynamicParameters.Add("@OrderDate", orderRequest?.OrderDate, DbType.Date, ParameterDirection.Input);
                    dynamicParameters.Add("@SalesOrderNumber", orderRequest?.SalesOrderNumber, DbType.Guid, direction: ParameterDirection.Input);
                    dynamicParameters.Add("@Quantity", orderRequest.Quantity, DbType.Int32, direction: ParameterDirection.Input);

                    return dynamicParameters;
                });
            }
            catch
            {
                throw;
            }
        }
    }
}