using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using VirtualShopping.API.SignalR;
using VirtualShopping.BLL.Interface;
using VirtualShopping.Domain.Requests;
using VirtualShopping.Domain.Requests.Order;
using VirtualShopping.Domain.Responses.Cart;
using VirtualShopping.Domain.Responses.Order;
using VirtualShopping.Domain.Utilities;
using VirtualShopping.SignalR;

namespace VirtualShopping.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ICartServices _cartServices;
        private readonly IHubContext<CartsHub> _cartHub;
        private readonly IHubContext<OrdersHub> _orderHub;
        private readonly IHubContext<ShopsHub> _shopHub;
        private readonly GroupsTracker _tracker;

        public OrderController(ICartServices cartServices,
                                IHubContext<OrdersHub> orderHub,
                                IHubContext<ShopsHub> shopHub,
                                IHubContext<CartsHub> cartHub,
                                GroupsTracker tracker)
        {

            _cartServices = cartServices;
            _cartHub = cartHub;
            _orderHub = orderHub;
            _shopHub = shopHub;
            _tracker = tracker;
        }

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(string orderId) {
            var order = await _cartServices.GetOrderById(orderId);
            if(order != null)
            {
                return Ok(order);
            }
            return NotFound();
        }

        /// <summary>
        /// Placed new order
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response of placed new order progress</returns>
        [HttpPost()]
        public async Task<ActionResult<PlacedNewOrderResModel>> PlacedNewOrder(PlacedNewOrderReq request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.PlacedNewOrder(request);
                if (response.IsSuccess)
                {
                    var shopConnectionIds = await _tracker.GetUserConnectionsOfShop(response.ShopId);
                    await _shopHub.Clients.Clients(shopConnectionIds)
                            .SendAsync(HubMethodConstants.NewOrder, response);
                    var customerIds = await _tracker.GetUserConnectionsOfCart(response.OrderId);
                    await _cartHub.Clients.Clients(customerIds)
                            .SendAsync(HubMethodConstants.NewOrder, response);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response </returns>
        [HttpPut("cancel")]
        public async Task<ActionResult<CancelOrderResModel>> CancelOrder(CancelOrderReq request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.CancelOrder(request);
                if (response.IsSuccess)
                {
                    var shopConnectionIds = await _tracker.GetUserConnectionsOfShop(response.ShopId);
                    await _shopHub.Clients.Clients(shopConnectionIds)
                            .SendAsync(HubMethodConstants.CancelOrder, response.OrderId);
                    var customerIds = await _tracker.GetUserConnectionsOfOrder(response.OrderId);
                    await _orderHub.Clients.Clients(customerIds)
                            .SendAsync(HubMethodConstants.CancelOrder, response.OrderId);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Update status of order for customer or shop (not include cancel order)
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response progress of update status for order</returns>
        [HttpPut("status")]
        public async Task<ActionResult<ChangeOrderStatusResModel>> ChangeOrderStatus(ChangeOrderStatusReqModel request)
        {
            if (ModelState.IsValid && request.OrderStatus != OrderStatusConstants.Cancelled)
            {
                var response = await _cartServices.ChangeOrderStatus(request);
                if (response.IsSuccess)
                {
                    var responseData = new ChangeOrderStatusHubModel
                    {
                        OrderId = response.OrderId,
                        NewStatus = response.NewStatus
                    };

                    var customerIds = await _tracker.GetUserConnectionsOfOrder(response.OrderId);
                    await _orderHub.Clients.Clients(customerIds)
                            .SendAsync(HubMethodConstants.ChangeOrderStatus, responseData);
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Gets All Orders of Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>List of orders belong to specific customer</returns>
        [HttpGet("{customerId}/customer/all")]
        public async Task<IActionResult> GetAllOrdersOfCustomer(string customerId)
        {

            return Ok(await _cartServices.GetAllOrdersByCustomerId(customerId));
        }

        /// <summary>
        /// Gets All Orders of Shop
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns>List of orders belong to specific shop</returns>
        [HttpGet("{shopId}/shop/all")]
        public async Task<IActionResult> GetAllOrdersOfShop(string shopId)
        {

            return Ok(await _cartServices.GetAllOrdersByShopId(shopId));
        }
    }
}
