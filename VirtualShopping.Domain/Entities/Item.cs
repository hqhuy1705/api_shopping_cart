using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Entities
{
    public class Item
    {
        [Key]
        [MaxLength(length:6)]
        public string ItemId { get; set; }
        [Required]
        [MaxLength(length:100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ShopId")]
        public string ShopId { get; set; }
        public ICollection<CartItem> CartItemList { get; set; }

    }
}