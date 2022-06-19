using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.ItemDTO;
using VirtualShopping.Domain.Responses.Item;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.BLL.Implement
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemDTO> Create(ItemCreateDTO dto)
        {
            var shop = await _unitOfWork.ShopRepository.GetShopByIDAsync(dto.ShopId);
            if (shop == null)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.NotFoundShop
                };
            }

            Item item = new Item
            {
                ItemId = Helper.IdGenerator(),
                ShopId = dto.ShopId,
                Name = dto.Name,
                Price = dto.Price,
                Image = !String.IsNullOrEmpty(dto.Image?.FileName) ? await Helper.ImgConvertor(dto.Image) : "",
                IsActive = true,
            };
           
            return !await _unitOfWork.ItemRepository.Create(item)
                ? new ItemDTO
                {
                    ErrorMessage = "Create Item unsucessful."
                }
                : new ItemDTO
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                    IsActive = item.IsActive
                };
        }

        public async Task<ItemDTO> Delete(ItemDeleteDTO dto)
        {
            if (await _unitOfWork.ShopRepository.GetShopByIDAsync(dto.ShopId) == null)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }
            var item = await _unitOfWork.ItemRepository.GetItemByIDAsync(dto.ItemId);
            if (item == null)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }

            if (item.ShopId != dto.ShopId)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.NotYourItem
                };
            }

            item.IsActive = false;

            return await _unitOfWork.ItemRepository.Update(item)
                ? new ItemDTO
                {
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                    IsActive = item.IsActive,
                    ShopId = item.ShopId
                }
                : new ItemDTO
                {
                    ErrorMessage = "Delete Unsuccessful."
                };
        }

        public async Task<ItemDTO> GetItemById(string id)
        {
            Item item = await _unitOfWork.ItemRepository.GetItemByIDAsync(id);
            return item != null ?
                new ItemDTO
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                    ShopId = item.ShopId
                }
                : new ItemDTO
                {
                    ErrorMessage = ErrorConstants.NotFoundItem
                };
        }

        public async Task<ItemDTO> Update(ItemUpdateDTO dto)
        {
            if (await _unitOfWork.ShopRepository.GetShopByIDAsync(dto.ShopId) == null)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }
            var item = await _unitOfWork.ItemRepository.GetItemByIDAsync(dto.ItemId);
            if (item == null)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }

            if (item.ShopId != dto.ShopId)
            {
                return new ItemDTO
                {
                    ErrorMessage = ErrorConstants.NotYourItem
                };
            }

            item.Image = String.IsNullOrEmpty(dto.Image?.FileName) ? item.Image : await Helper.ImgConvertor(dto.Image);
            item.Name = String.IsNullOrEmpty(dto.Name) ? item.Name : dto.Name;
            item.Price = dto.Price;

            return await _unitOfWork.ItemRepository.Update(item)
                ? new ItemDTO
                {
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image
                }
                : new ItemDTO
                {
                    ErrorMessage = "Update Unsuccessful."
                };
        }

        public async Task<ActiveItemResModel> ActiveItem(ActiveItemReqModel request)
        {
            var item = await _unitOfWork.ItemRepository.GetItemByIDAsync(request.ItemId);
            
            if(item == null)
            {
                return new ActiveItemResModel(ErrorConstants.NotFoundItem);
            }

            if (item.ShopId != request.ShopId)
            {
                return new ActiveItemResModel(ErrorConstants.NotYourItem);
            }

            item.IsActive = true;

            var result = await _unitOfWork.ItemRepository.Update(item);
            return result
                ? new ActiveItemResModel
                {
                    ItemId = item.ItemId
                }
                : new ActiveItemResModel(ErrorConstants.UnknownError);
        }
    }
}
