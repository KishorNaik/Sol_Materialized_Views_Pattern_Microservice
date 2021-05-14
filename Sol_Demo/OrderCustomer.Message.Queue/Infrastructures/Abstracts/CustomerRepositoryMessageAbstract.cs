using Dapper;
using OrderCustomer.Message.Queue.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCustomer.Message.Queue.Infrastructures.Abstracts
{
    public abstract class CustomerRepositoryMessageAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, CustomerMessageDTO customerDTO)
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
    }
}