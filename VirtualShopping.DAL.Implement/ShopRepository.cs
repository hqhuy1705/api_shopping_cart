using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.DAL.Implement.Data;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Shop;

namespace VirtualShopping.DAL.Implement
{
    public class ShopRepository : IShopRepository
    {
        private readonly DataContext _context;

        public ShopRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(string phoneNumber)
        {
            var shop = await GetShopByPhoneNumAsync(phoneNumber);
            shop.IsActived = false;
            return await Update(shop);
        }

        public async Task<ShopInfoDTO> GetShopByIDAsync(string id)
        {
            return await _context.Shops.Where(x => x.ShopId == id)
                .Select(x => new ShopInfoDTO
                {
                    Name = x.Name,
                    Items = x.Items,
                    Image = x.Image,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefaultAsync();
                
        }

        public async Task<Shop> GetShopByPhoneNumAsync(string phoneNumber)
        {
            return await _context.Shops.SingleOrDefaultAsync(shop => shop.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<ShopInfoDTO>> GetShopsAsync()
        {
            return await _context.Shops.Where(s => s.IsActived)
                .Select(x => new ShopInfoDTO
                {
                    ShopId = x.ShopId,
                    Name = x.Name,
                    Image = x.Image,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync();
        }

        public async Task<bool> Register(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Shop shop)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
