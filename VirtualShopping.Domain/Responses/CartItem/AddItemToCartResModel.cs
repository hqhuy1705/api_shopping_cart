using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Requests.CartItem;

namespace VirtualShopping.Domain.Responses.CartItem
{
    public class AddItemToCartResModel
    {
        [Required]
        [MaxLength(6)]
        public string ItemId { get; set; }
        public int Amount { get; set; }
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(ItemId);
        public string ErrorMessage { get; set; }
    }
}
