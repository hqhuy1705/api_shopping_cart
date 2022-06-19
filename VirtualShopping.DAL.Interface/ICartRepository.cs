using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests;
using VirtualShopping.Domain.Responses.Order;
using VirtualShopping.Domain.Responses.Cart;
using VirtualShopping.Domain.Requests.CartItem;

namespace VirtualShopping.DAL.Interface
{
    public interface ICartRepository
    {
        Task<GetCartResModel> GetCartForClientById(string cartId, bool getShop);
        Task<Cart> GetCartEntityById(string cartId);
        Task<bool> CreateCart(Cart request);
        Task<bool> PlacedNewOrder(PlacedNewOrderReq request, Cart existCart);
        Task<GetOrderResModel> GetOrderForClientById(string orderId);
        Task<Cart> GetOrderEntityById(string orderId);
        Task<IEnumerable<GetOrderResModel>> GetAllOrdersByCustomerId(string customerId);
        Task<bool> UpdateOrderStatus(Cart order, string newStatus);
        Task<IEnumerable<GetOrderResModel>> GetAllOrdersByShopId(string shopId);
        Task<IEnumerable<CartItem>> GetCustomerAllItems(GetCustomerAllItemReq request);
        Task<GetCartResModel> GetExistCartByShopIdAndCustomerId(string shopId, string customerId);
    }
}