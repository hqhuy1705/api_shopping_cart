using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.CartItem
{
    public class ItemInCartModel
    {
        [Required]
        [Range(0, 1000)]
        public int Amount { get ; set; }
        [Required]
        public string ItemId { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
