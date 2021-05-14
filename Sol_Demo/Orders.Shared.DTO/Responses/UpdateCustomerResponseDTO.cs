using Customer.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Shared.DTO.Responses
{
    public interface IUpdateNewCustomerResponseDTO : ICustomerEntity
    {
    }

    public class UpdateNewCustomerResponseDTO : IUpdateNewCustomerResponseDTO
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String MobileNo { get; set; }
    }

    public class UpdateOldCustomerResponseDTO
    {
        public String FirstNameOldValue { get; set; }

        public String LastNameOldValue { get; set; }

        public String MobileNoOldValue { get; set; }
    }

    public class UpdateCustomerResponseDTO
    {
        public UpdateNewCustomerResponseDTO UpdateNewCustomerResponse { get; set; }

        public UpdateOldCustomerResponseDTO UpdateOldCustomerResponse { get; set; }
    }
}