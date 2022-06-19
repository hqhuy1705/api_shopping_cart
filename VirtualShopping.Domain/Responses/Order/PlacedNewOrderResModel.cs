using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.Domain.Responses.CartItem;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class PlacedNewOrderResModel
    {
        public string OrderId { get; set; }
        public string ShopId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public IEnumerable<ItemInCartViewModel> ItemsInCart { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryInformation { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(OrderId);
        public string ErrorMessage {get; set;}
    }
}