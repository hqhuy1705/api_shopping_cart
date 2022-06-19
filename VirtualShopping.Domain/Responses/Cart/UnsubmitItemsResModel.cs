using System;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class UnsubmitItemsResModel
    {
        public bool IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }

        public UnsubmitItemsResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public UnsubmitItemsResModel(){}
    }
}