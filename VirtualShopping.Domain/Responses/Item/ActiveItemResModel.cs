using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Item
{
    public class ActiveItemResModel
    {
        public string ItemId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSucess => String.IsNullOrEmpty(ErrorMessage);
        public ActiveItemResModel()
        {

        }

        public ActiveItemResModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
