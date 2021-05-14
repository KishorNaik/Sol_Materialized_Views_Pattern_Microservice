using Dapper;
using Product.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Product.Shared.DTO.Requests;

namespace Product.Command.Api.Infrastructures.Abstracts
{
    public abstract class ProductRepositoryCommandAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, IProductRequestDTO productDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    DynamicParameters dynamicParameters = new();

                    dynamicParameters.Add("@Command", command, DbType.String, ParameterDirection.Input);

                    dynamicParameters.Add("@ProductIdentity", productDTO.ProductIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameters.Add("@ProductName", productDTO.ProductName, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@UnitPrice", productDTO.UnitPrice, DbType.Double, ParameterDirection.Input);

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