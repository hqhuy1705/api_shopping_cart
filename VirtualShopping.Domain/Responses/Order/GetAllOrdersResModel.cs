using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Order;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class GetAllOrdersResModel
    {
        public IEnumerable<GetOrderResModel> Orders { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => Orders != null && Orders.Count() > 0;
    }
}