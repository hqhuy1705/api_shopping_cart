using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class RemovedCustomerSignalrRes
    {
        public string CustomerIdRemoved { get; set; }
        public string CartId { get; set; }
    }
}
