using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Requests
{
    public class GetAllCartsReq
    {
        public string CustomerId { get; set; }
    }
}