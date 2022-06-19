using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class CancelOrderResModel
    {
        public string OrderId { get; set; }
        public string ShopId { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(OrderId);
        public string ErrorMessage { get; set;}

        public CancelOrderResModel()
        {

        }

        public CancelOrderResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}