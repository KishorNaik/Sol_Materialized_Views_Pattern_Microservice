using Orders.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Shared.DTO.Requests
{
    public interface IOrderRequestDTO : IOrdersEntity
    {
    }

    public class OrderRequestDTO : IOrderRequestDTO
    {
        public Guid? OrderIdentity { get; set; }

        public Guid? CustomerIdentity { get; set; }

        public Guid? ProductIdentity { get; set; }

        public Guid? SalesOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Quantity { get; set; }
    }
}