using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualShopping.Domain.Requests.Customer
{
    public class GetCartReq
    {
        [Key]
        [MaxLength(6)]
        public string CartId {get; set; }
    }
}