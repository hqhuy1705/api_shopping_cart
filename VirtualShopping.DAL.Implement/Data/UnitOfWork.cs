using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.DAL.Interface;

namespace VirtualShopping.DAL.Implement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository => new CustomerRepository(_context);
        public IShopRepository ShopRepository => new ShopRepository(_context);
        public IItemRepository ItemRepository => new ItemRepository(_context);
        public ICartRepository CartRepository => new CartRepository(_context);
        public ICartItemRepository CartItemRepository => new CarItemRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
