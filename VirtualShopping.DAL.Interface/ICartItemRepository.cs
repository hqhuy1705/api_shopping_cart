using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;

namespace VirtualShopping.DAL.Interface
{
    public interface ICartItemRepository
    {
        Task AddNewItemIntoCartAsync(CartItem item);
        Task<List<CartItem>> GetItemsFromCartByCartIdAsync(string cartId);
        Task<CartItem> GetItemFromCartAsync(string cartId, string itemId, string customerId);
        Task<bool> ToggleItemReadyAsync (int CartItemId);
    }
}
