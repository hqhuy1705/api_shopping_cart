using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Item;

namespace VirtualShopping.DAL.Interface
{
    public interface IItemRepository
    {
        Task<Item> GetItemByIDAsync(string id);
        Task<bool> Create(Item item);
        Task<bool> Update(Item item);
    }
}
