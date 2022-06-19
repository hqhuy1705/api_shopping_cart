using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.DAL.Implement.Data;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Item;

namespace VirtualShopping.DAL.Implement
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Item item)
        {
            await _context.Items.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Item> GetItemByIDAsync(string id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<bool> Update(Item item)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
