using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Entities
{
    public class Shop
    {
        [Key]
        [MaxLength(length:6)]
        public string ShopId { get; set; }
        [Required]
        [MaxLength(length:100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(length:15)]
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public bool IsActived { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
