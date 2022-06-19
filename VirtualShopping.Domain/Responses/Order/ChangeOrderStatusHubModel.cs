using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Order
{
    public class ChangeOrderStatusHubModel
    {
        public string OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
