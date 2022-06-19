using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.ShopDTO
{
    public class ShopGetIdDTO
    {
        [Required]
        [MaxLength(length: 15)]
        public string PhoneNumber { get; set; }
    }
}
