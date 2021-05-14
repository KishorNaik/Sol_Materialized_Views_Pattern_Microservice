using System;

namespace Product.Shared.Entity
{
    public interface IProductEntity
    {
        Guid? ProductIdentity { get; set; }

        String ProductName { get; set; }

        double? UnitPrice { get; set; }
    }
}