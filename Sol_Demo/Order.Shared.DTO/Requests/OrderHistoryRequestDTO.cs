using Orders.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Shared.DTO.Requests
{
    public interface IOrderHistoryRequestDTO : IOrdersEntity
    {
        DateTime? FromOrderDate { get; set; }

        DateTime? ToOrderDate { get; set; }

        OrderPagination Pagination { get; set; }
    }

    public class OrderHistoryRequestDTO : IOrderHistoryRequestDTO
    {
        public Guid? OrderIdentity { get; set; }

        public Guid? CustomerIdentity { get; set; }

        public Guid? ProductIdentity { get; set; }

        public Guid? SalesOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Quantity { get; set; }

        public DateTime? FromOrderDate { get; set; }

        public DateTime? ToOrderDate { get; set; }

        public OrderPagination Pagination { get; set; }
    }
}