using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Requests.ItemDTO;
using VirtualShopping.Domain.Responses.Item;

namespace VirtualShopping.BLL.Interface
{
    public interface IItemService
    {
        Task<ItemDTO> Create(ItemCreateDTO dto);
        Task<ItemDTO> Update(ItemUpdateDTO dto);
        Task<ItemDTO> Delete(ItemDeleteDTO dto);
        Task<ItemDTO> GetItemById(string id);
        Task<ActiveItemResModel> ActiveItem(ActiveItemReqModel request);
    }
}
