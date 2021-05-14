using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Command.Api.Infrastructures.ResultSets
{
    public class UpdateProductResultSet
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }

        public String ProductNameOldValue { get; set; }

        public double? UnitPrice { get; set; }

        public double? UnitPriceOldValue { get; set; }
    }
}