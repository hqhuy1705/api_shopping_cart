using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.ShopDTO;
using VirtualShopping.Domain.Responses.Shop;

namespace VirtualShopping.BLL.Interface
{
    public interface IShopService
    {
        Task<ShopDTO> Register(ShopRegisterDTO dto);
        Task<ShopDTO> Login(ShopLoginDTO dto);
        Task<ShopDTO> Update(ShopUpdateDTO dto);
        Task<ShopDTO> Delete(ShopDeleteDTO dto);
        Task<ShopInfoDTO> GetShopInfoById(string id);
        Task<IEnumerable<ShopInfoDTO>> GetShops();
    }
}
