using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class RemoveCustomerResModel
    {
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CustomerIdToRemoved { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(CustomerIdToRemoved);
        public string ErrorMessage { get; set; }
    }
}
