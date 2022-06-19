using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.CartItem
{
    public class RemoveItemFromCartResModel
    {
        [Required]
        [MaxLength(6)]
        public string ItemId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(ItemId);
        public string ErrorMessage { get; set; }
        public RemoveItemFromCartResModel()
        {

        }
        public RemoveItemFromCartResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
