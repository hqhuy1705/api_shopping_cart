using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.ItemDTO
{
    public class ItemCreateDTO
    {
        [Required]
        [MaxLength(6)]
        public string ShopId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
