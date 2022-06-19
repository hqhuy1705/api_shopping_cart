using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Domain.Requests.Customer
{
    public class CustomerLoginReqModel
    {
        [MaxLength(15)]
        [Required]
        [RegularExpression(RegexConstants.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
