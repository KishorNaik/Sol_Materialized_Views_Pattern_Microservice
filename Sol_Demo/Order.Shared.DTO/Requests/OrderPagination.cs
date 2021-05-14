using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Shared.DTO.Requests
{
    public class OrderPagination
    {
        public int? PageNumber { get; set; }

        public int? RowsOfPage { get; set; }
    }
}