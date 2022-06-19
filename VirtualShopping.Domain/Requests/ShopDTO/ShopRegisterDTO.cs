using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.ShopDTO
{
    public class ShopRegisterDTO
    {
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}