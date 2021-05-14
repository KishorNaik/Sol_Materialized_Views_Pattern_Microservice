using Dapper;
using OrderProduct.Message.Queue.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Message.Queue.Infrastructures.Abstracts
{
    public abstract class ProductRepositoryMessageAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, IProductMessageDTO productMessageDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    DynamicParameters dynamicParameters = new();

                    dynamicParameters.Add("@Command", command, DbType.String, ParameterDirection.Input);

                    dynamicParameters.Add("@ProductIdentity", productMessageDTO.ProductIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameters.Add("@ProductName", productMessageDTO.ProductName, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@UnitPrice", productMessageDTO.UnitPrice, DbType.Double, ParameterDirection.Input);

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