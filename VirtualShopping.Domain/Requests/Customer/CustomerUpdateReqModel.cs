using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Domain.Requests.Customer
{
    public class CustomerUpdateReqModel
    {
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(15)]
        [Required]
        [RegularExpression(RegexConstants.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
