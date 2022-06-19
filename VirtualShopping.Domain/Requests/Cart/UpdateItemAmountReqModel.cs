using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests.Cart
{
    public class UpdateItemAmountReqModel
    {
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(6)]
        public string ItemId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Amount { get; set; }
    }
}
