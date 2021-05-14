using Customer.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCustomer.Message.Queue.Api.DTOs
{
    public interface ICustomerMessageDTO : ICustomerEntity
    {
    }

    public class CustomerMessageDTO : ICustomerMessageDTO
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String MobileNo { get; set; }
    }
}