using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.Cart
{
    public class RemoveCustomerReqModel
    {
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CustomerIdToRemoved { get; set; }
    }
}
