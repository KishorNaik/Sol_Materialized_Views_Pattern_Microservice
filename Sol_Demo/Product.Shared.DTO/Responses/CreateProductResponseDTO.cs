using Product.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Shared.DTO.Responses
{
    public interface ICreateProductResponseDTO : IProductEntity
    {
    }

    public class CreateProductResponseDTO : ICreateProductResponseDTO
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }
}