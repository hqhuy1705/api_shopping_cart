using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VirtualShopping.Domain.Responses.Shop
{
    public class ShopDTO
    {
        public string ShopId { get; set; }
        public string PhoneNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}