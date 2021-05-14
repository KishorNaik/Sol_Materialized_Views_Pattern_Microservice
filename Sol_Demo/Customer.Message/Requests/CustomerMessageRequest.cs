using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Message.Requests
{
    public class CustomerMessageRequest
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String MobileNo { get; set; }
    }
}