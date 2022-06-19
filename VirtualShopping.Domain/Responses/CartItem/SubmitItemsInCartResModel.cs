using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.CartItem
{
    public class SubmitItemsInCartResModel
    {
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }
        public SubmitItemsInCartResModel()
        {

        }

        public SubmitItemsInCartResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
