using System;

namespace Orders.Shared.Entity
{
    public interface IOrdersEntity
    {
        Guid? OrderIdentity { get; set; }

        Guid? CustomerIdentity { get; set; }

        Guid? ProductIdentity { get; set; }

        Guid? SalesOrderNumber { get; set; }

        DateTime? OrderDate { get; set; }

        int? Quantity { get; set; }
    }
}