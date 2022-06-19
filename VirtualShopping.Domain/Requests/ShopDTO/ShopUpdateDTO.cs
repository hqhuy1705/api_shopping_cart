using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VirtualShopping.Domain.Entities;

namespace VirtualShopping.Domain.Requests.ShopDTO
{
    public class ShopUpdateDTO
    {
        [Required]
        [MaxLength(length:100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(length:15)]
        public string PhoneNumber { get; set; }
        [MaxLength(length:15)]
        public string NewPhoneNumber { get; set; }
        public IFormFile Logo { get; set; }
    }
}