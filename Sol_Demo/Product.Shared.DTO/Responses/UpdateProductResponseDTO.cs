using Product.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Shared.DTO.Responses
{
    public interface IUpdateProductResponseDTO : IProductEntity
    {
    }

    public class UpdateNewProductResponseDTO : IUpdateProductResponseDTO
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }

    public class UpdateOldProductResponseDTO
    {
        public String ProductNameOldValue { get; set; }

        public double? UnitPriceOldValue { get; set; }
    }

    public class UpdateProductResponseDTO
    {
        public UpdateNewProductResponseDTO UpdateNewProductResponse { get; set; }

        public UpdateOldProductResponseDTO UpdateOldProductResponse { get; set; }
    }
}