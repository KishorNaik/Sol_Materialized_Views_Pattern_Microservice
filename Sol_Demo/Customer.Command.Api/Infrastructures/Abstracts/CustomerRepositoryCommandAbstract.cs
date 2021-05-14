using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Customer.Shared.DTO.Requests;

namespace Customer.Command.Api.Infrastructures.Abstracts
{
    public abstract class CustomerRepositoryCommandAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, ICustomerRequestDTO customerDTO = null)
        {
            try
            {
                return Task.Run(() =>
                {
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("@Command", command, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@CustomerIdentity", customerDTO?.CustomerIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameters.Add("@FirstName", customerDTO?.FirstName, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@LastName", customerDTO?.LastName, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@MobileNo", customerDTO?.MobileNo, DbType.String, ParameterDirection.Input);

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