using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.DAL.Implement.Data;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;

namespace VirtualShopping.DAL.Implement
{
    public class CarItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CarItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddNewItemIntoCartAsync(CartItem item)
        {
            await _context.CartsItems.AddAsync(item);
        }

        public async Task<CartItem> GetItemFromCartAsync(string cartId, string itemId, string customerId)
        {
            return await _context.CartsItems
                                .Where(i => i.ItemId == itemId 
                                            && i.CartId == cartId 
                                            && !i.IsDeleted 
                                            && i.CustomerId == customerId)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<CartItem>> GetItemsFromCartByCartIdAsync(string cartId)
        {
            return await _context.CartsItems
                    .Include(i => i.Item)
                    .Include(i => i.Customer)
                    .Where(i => i.CartId == cartId && !i.IsDeleted)
                    .ToListAsync();
        }

        public async Task<bool> ToggleItemReadyAsync(int cartItemId) {

            var cartItem = await _context.CartsItems.FindAsync(cartItemId);

            if (cartItem == null) return false;

            cartItem.ReadyToOrder = false;

            return true;
        }
    }
}
