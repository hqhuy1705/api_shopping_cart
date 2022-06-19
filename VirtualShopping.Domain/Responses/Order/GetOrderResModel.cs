using System;
using System.Collections.Generic;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.CartItem;

namespace VirtualShopping.Domain.Responses.Order
{
    public class GetOrderResModel
    {
        public string ShopId { get; set; }
        public string OrderId { get; set; }
        public string ShopName { get; set; }
        public string PhoneNumberOfShop { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime? OrderTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string DeliveryInformation { get; set; }
        public IEnumerable<ItemInCartViewModel> ItemsInCart { get; set; }
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (var item in ItemsInCart)
                {
                    totalPrice += item.Price * item.Amount;
                }
                return totalPrice;
            }
        }
        public string ErrorMessage { get; set; }
    }
}