using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Domain.Responses.Customer
{
    public class CustomerRegisterResModel
    {
        public string CustomerId { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(CustomerId);
        public string ErrorMessage { get; set; }
    }
}
