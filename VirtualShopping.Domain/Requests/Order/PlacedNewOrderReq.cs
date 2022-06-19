using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VirtualShopping.Domain.Entities;

namespace VirtualShopping.Domain.Requests
{
    public class PlacedNewOrderReq
    {
        [Key]
        [MaxLength(6)]
        public string CartId { get ; set ; }
        [Required]
        public string DeliveryInformation {  get; set; }

    }
}