using System;

namespace Customer.Shared.Entity
{
    public interface ICustomerEntity
    {
        Guid? CustomerIdentity { get; set; }

        String FirstName { get; set; }

        String LastName { get; set; }

        String MobileNo { get; set; }
    }
}