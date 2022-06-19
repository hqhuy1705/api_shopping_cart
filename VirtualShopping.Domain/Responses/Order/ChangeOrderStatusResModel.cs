using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Order
{
    public class ChangeOrderStatusResModel
    {
        public string OrderId { get; set; }
        public string ShopId { get; set; }
        public string NewStatus { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => !String.IsNullOrEmpty(OrderId);

        public ChangeOrderStatusResModel()
        {

        }

        public ChangeOrderStatusResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
