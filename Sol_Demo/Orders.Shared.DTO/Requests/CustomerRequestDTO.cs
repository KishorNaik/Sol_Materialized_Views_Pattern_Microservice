using Customer.Shared.Entity;
using System;
using System.Runtime.Serialization;

namespace Customer.Shared.DTO.Requests
{
    public interface ICustomerRequestDTO : ICustomerEntity
    {
    }

    [DataContract]
    public class CustomerRequestDTO : ICustomerRequestDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CustomerIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String MobileNo { get; set; }
    }
}