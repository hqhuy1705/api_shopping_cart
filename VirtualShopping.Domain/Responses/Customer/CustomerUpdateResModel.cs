using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Customer
{
    public class CustomerUpdateResModel
    {
        public string CustomerId { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(CustomerId);
        public string ErrorMessage { get; set; }
    }
}
