using Product.Shared.Entity;
using System;

namespace Product.Shared.DTO.Requests
{
    public interface IProductRequestDTO : IProductEntity
    {
    }

    public class ProductRequestDTO : IProductRequestDTO
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }
}