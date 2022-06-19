using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Requests.CartItem;

namespace VirtualShopping.Domain.Responses.Cart
{
    public class AddItemsToCartHubModel
    {
        public IEnumerable<ItemInCartModel> Items { get; set; }
        public string CartId { get; set; }
    }
}
