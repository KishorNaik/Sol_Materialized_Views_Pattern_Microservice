using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Message.Request
{
    public class ProductMessageRequest
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public double? UnitPrice { get; set; }
    }
}