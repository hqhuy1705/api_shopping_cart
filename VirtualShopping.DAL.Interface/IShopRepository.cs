using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Shop;

namespace VirtualShopping.DAL.Interface
{
    public interface IShopRepository
    { 
        Task<ShopInfoDTO> GetShopByIDAsync(string id);
        Task<Shop> GetShopByPhoneNumAsync(string phoneNumber);
        Task<bool> DeleteAsync(string phoneNumber);
        Task<IEnumerable<ShopInfoDTO>> GetShopsAsync();
        Task<bool> Register(Shop shop);
        Task<bool> Update(Shop shop);
    }
}
