using Customer.Shared.Entity;
using System;
using System.Runtime.Serialization;

namespace Customer.Shared.DTO.Requests
{
    public interface ICreateCustomerResponseDTO : ICustomerEntity
    {
    }

    public class CreateCustomerResponseDTO : ICreateCustomerResponseDTO
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String MobileNo { get; set; }
    }
}