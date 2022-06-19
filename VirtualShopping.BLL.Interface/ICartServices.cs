using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests;
using VirtualShopping.Domain.Requests.Customer;
using VirtualShopping.Domain.Responses.Cart;
using VirtualShopping.Domain.Responses.Order;
using VirtualShopping.Domain.Requests.CartItem;
using VirtualShopping.Domain.Responses.CartItem;
using VirtualShopping.Domain.Requests.Order;
using VirtualShopping.Domain.Requests.Cart;

namespace VirtualShopping.BLL.Interface
{
    public interface ICartServices
    {
        Task<GetCartResModel> GetCartById(string cartId, bool getShop);
        Task<GetAllOrdersResModel> GetAllOrdersByCustomerId(string customerId);
        Task<GetOrderResModel> GetOrderById(string orderId);
        Task<CreateCartResModel> CreateCart(CreateCartReq request);
        Task<PlacedNewOrderResModel> PlacedNewOrder(PlacedNewOrderReq request);
        Task<CancelOrderResModel> CancelOrder(CancelOrderReq request);
        Task<SubmitItemsInCartResModel> SubmitItemsInCartAsync(SubmitItemsInCartReqModel request);
        Task<ChangeOrderStatusResModel> ChangeOrderStatus(ChangeOrderStatusReqModel request);
        Task<GetAllOrdersResModel> GetAllOrdersByShopId(string customerId);
        Task<UnsubmitItemsResModel> UnsubmitItems (UnsubmitItemsReq request);
        Task<GetCartResModel> GetExistCartByShopIdAndCustomerId(GetExistCartReqModel request);
        Task<RemoveCustomerResModel> RemoveCustomerFromCart(RemoveCustomerReqModel request);
        Task<AddItemToCartResModel> AddItemToCart(AddItemToCartReqModel request);
        Task<RemoveItemFromCartResModel> RemoveItemFromCart(RemoveItemFromCartReqModel request);
    }
}