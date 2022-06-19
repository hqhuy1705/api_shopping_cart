using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualShopping.Domain.Requests
{
    public class CancelOrderReq
    {
        [Required]
        [MaxLength(6)]
        public string OrderId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
    }
}