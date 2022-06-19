using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.ShopDTO;
using VirtualShopping.Domain.Responses.Shop;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.BLL.Implement
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShopDTO> Delete(ShopDeleteDTO dto)
        {
            var shop = await _unitOfWork.ShopRepository.GetShopByPhoneNumAsync(dto.PhoneNumber);
            if (shop == default)
            {
                return new ShopDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }
            return await _unitOfWork.ShopRepository.DeleteAsync(dto.PhoneNumber)
                ? new ShopDTO
                {
                    PhoneNumber = dto.PhoneNumber
                }
                : new ShopDTO
                {
                    ErrorMessage = "Delete Unsuccessful."
                };
        }

        public async Task<ShopDTO> Login(ShopLoginDTO dto)
        {
            var shop = await _unitOfWork.ShopRepository.GetShopByPhoneNumAsync(dto.PhoneNumber);
            return (shop == default || !shop.IsActived)
                ? new ShopDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                }
                : new ShopDTO
                {
                    ShopId = shop.ShopId,
                    PhoneNumber = shop.PhoneNumber
                };
        }

        public async Task<ShopDTO> Register(ShopRegisterDTO dto)
        {
            var shop = await _unitOfWork.ShopRepository.GetShopByPhoneNumAsync(dto.PhoneNumber);
            if ( shop != null )
            {
                if (shop.IsActived)
                {
                    return new ShopDTO
                    {
                        ErrorMessage = ErrorConstants.DuplicatePN
                    };
                } else
                {
                    shop.Name = dto.Name;
                    shop.Image = !String.IsNullOrEmpty(dto.Logo?.FileName) ? await Helper.ImgConvertor(dto.Logo) : null;
                    shop.IsActived = true;
                }
                return await _unitOfWork.ShopRepository.Update(shop)
                ? new ShopDTO
                {
                    PhoneNumber = shop.PhoneNumber
                }
                : new ShopDTO
                {
                    ErrorMessage = "Reactivate Unsuccessful."
                };

            }

            var res = new Shop
            {
                ShopId = Helper.IdGenerator(),
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Image = !String.IsNullOrEmpty(dto.Logo?.FileName) ? await Helper.ImgConvertor(dto.Logo) : null,
                IsActived = true,
            };

            return await _unitOfWork.ShopRepository.Register(res)
                ? new ShopDTO
                {
                    ShopId = res.ShopId,
                    PhoneNumber = res.PhoneNumber
                }
                : new ShopDTO
                {
                    ErrorMessage = "Register Unsuccessful."
                };
        }

        public async Task<ShopDTO> Update(ShopUpdateDTO dto)
        {
            if (await _unitOfWork.ShopRepository.GetShopByPhoneNumAsync(dto.NewPhoneNumber) != null)
            {
                return new ShopDTO
                {
                    ErrorMessage = ErrorConstants.DuplicatePN
                };
            }
            Shop shop = await _unitOfWork.ShopRepository.GetShopByPhoneNumAsync(dto.PhoneNumber);
            if (shop == null)
            {
                return new ShopDTO
                {
                    ErrorMessage = ErrorConstants.InvalidPN
                };
            }

            shop.Image = !String.IsNullOrEmpty(dto.Logo?.FileName) ? await Helper.ImgConvertor(dto.Logo) : shop.Image;
            shop.Name = dto.Name;
            shop.PhoneNumber = String.IsNullOrEmpty(dto.NewPhoneNumber)? dto.PhoneNumber: dto.NewPhoneNumber;

            return await _unitOfWork.ShopRepository.Update(shop)
                ? new ShopDTO
                {
                    PhoneNumber = shop.PhoneNumber
                }
                : new ShopDTO
                {
                    ErrorMessage = "Update Unsuccessful."
                };
        }

        public async Task<ShopInfoDTO> GetShopInfoById(string id)
        {
            var shop = await _unitOfWork.ShopRepository.GetShopByIDAsync(id);
            return shop != null ? shop
                : new ShopInfoDTO
                {
                    ErrorMessage = "Shop not found."
                };
        }

        public async Task<IEnumerable<ShopInfoDTO>> GetShops()
        {
            return await _unitOfWork.ShopRepository.GetShopsAsync();
        }
    }
}
