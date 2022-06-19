using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class CreateCartResModel
    {
        public string CartId { get; set; }
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage {get; set;}
    }
}