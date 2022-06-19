using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Domain.Requests.Order
{
    public class ChangeOrderStatusReqModel
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        public string CustomerId { get; set; }
        public string ShopId { get; set; }
    }
}
