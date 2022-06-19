using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualShopping.Domain.Requests
{
    public class CreateCartReq
    {
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(6)]
        public string ShopId { get; set; }
    }
}