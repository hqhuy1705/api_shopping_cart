using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.ItemDTO
{
    public class ItemUpdateDTO
    {
        [Required]
        [MaxLength(6)]
        public string ShopId { get; set; }
        [Required]
        [MaxLength(6)]
        public string ItemId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
