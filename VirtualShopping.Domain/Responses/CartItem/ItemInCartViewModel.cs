using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.CartItem
{
    public class ItemInCartViewModel
    {
        public string Image { get; set; }
        public int Amount { get ; set; }
        public double Price { get ; set ; }
        public string CustomerId { get; set; }
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public bool IsDeleted { get; set ; }
        public bool ReadyToOrder { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public bool ItemIsActive { get; set; }
    }
}
