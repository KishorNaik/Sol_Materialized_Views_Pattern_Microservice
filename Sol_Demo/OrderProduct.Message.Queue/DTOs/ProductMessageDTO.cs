using Product.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Message.Queue.DTOs
{
    public interface IProductMessageDTO : IProductEntity
    {
    }

    public class ProductMessageDTO : IProductMessageDTO
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }
}