using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.CartItem;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class GetCartResModel
    {
        public string CartId { get; set; }
        public string ShopId { get; set; }
        public VirtualShopping.Domain.Entities.Shop Shop { get; set; }
        public string CustomerId { get; set; }
        public double TotalPrice
        { get
            {
                if (ItemsInCart == null) return 0;
                double totalPrice = 0;
                foreach (var item in ItemsInCart)
                {
                    totalPrice += item.Price * item.Amount;
                }
                return totalPrice;
            }
        }
        public IEnumerable<ItemInCartViewModel> ItemsInCart { get; set; }
        public string ErrorMessage {get; set;}
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
    }
}