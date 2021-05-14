using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Command.Api.Infrastructures.ResultSets
{
    public class UpdateCustomerResultSet
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String MobileNo { get; set; }

        public String FirstNameOldValue { get; set; }

        public String LastNameOldValue { get; set; }

        public String MobileNoOldValue { get; set; }
    }
}