using Orders.Shared.Entity;
using Product.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Shared.DTO.Responses
{
    public class ProductResponseDTO : IProductEntity
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }

    public interface IOrderHistoryResponseDTO : IOrdersEntity
    {
        public ProductResponseDTO ProductResponse { get; set; }
    }

    public class OrderHistoryResponseDTO : IOrderHistoryResponseDTO
    {
        public Guid? OrderIdentity { get; set; }

        public Guid? CustomerIdentity { get; set; }

        public Guid? ProductIdentity { get; set; }

        public Guid? SalesOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Quantity { get; set; }

        public ProductResponseDTO ProductResponse { get; set; }
    }
}