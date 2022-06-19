using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Requests.CartItem;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class SubmitCartSignalrResponse
    {
        public string CartId { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<ItemInCartModel> Items { get; set; }
    }
}
