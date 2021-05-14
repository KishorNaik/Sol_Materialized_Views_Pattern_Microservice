using Dapper;
using Order.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Order.Query.Api.Infrastructures.Abstracts
{
    public abstract class OrderRepositoryQueryAbstract
    {
        protected Task<DynamicParameters> GetParameterAsync(string command, IOrderHistoryRequestDTO orderHistoryRequestDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    var dynamaicParameter = new DynamicParameters();
                    dynamaicParameter.Add("@Command", command, DbType.String, ParameterDirection.Input);
                    dynamaicParameter.Add("@CustomerIdentity", orderHistoryRequestDTO.CustomerIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamaicParameter.Add("@FromOrderDate", orderHistoryRequestDTO.FromOrderDate, DbType.Date, ParameterDirection.Input);
                    dynamaicParameter.Add("@ToOrderDate", orderHistoryRequestDTO.ToOrderDate, DbType.Date, ParameterDirection.Input);
                    dynamaicParameter.Add("@PageNumber", orderHistoryRequestDTO.Pagination.PageNumber, DbType.Int32, ParameterDirection.Input);
                    dynamaicParameter.Add("@RowsOfPage", orderHistoryRequestDTO.Pagination.RowsOfPage, DbType.Int32, ParameterDirection.Input);

                    return dynamaicParameter;
                });
            }
            catch
            {
                throw;
            }
        }
    }
}