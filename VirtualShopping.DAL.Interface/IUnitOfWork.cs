using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.DAL.Interface
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IShopRepository ShopRepository { get; }
        IItemRepository ItemRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
