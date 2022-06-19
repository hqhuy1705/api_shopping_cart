using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using VirtualShopping.API.SignalR;
using VirtualShopping.BLL.Interface;
using VirtualShopping.Domain.Requests;
using VirtualShopping.Domain.Requests.Cart;
using VirtualShopping.Domain.Requests.CartItem;
using VirtualShopping.Domain.Responses.Cart;
using VirtualShopping.Domain.Utilities;
using VirtualShopping.SignalR;

namespace VirtualShopping.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartServices _cartServices;
        private readonly IHubContext<CartsHub> _cartHub;
        private readonly GroupsTracker _tracker;

        public CartController(ICartServices cartServices,
                                IHubContext<CartsHub> cartHub,
                                GroupsTracker tracker)
        {
            _tracker = tracker;
            _cartServices = cartServices;
            _tracker = tracker;
            _cartHub = cartHub;
        }

        /// <summary>
        /// Gets Cart By Id
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="getShop"></param>
        /// <returns></returns>
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(string cartId, bool getShop = false)
        {
            var response = await _cartServices.GetCartById(cartId, getShop);
            
            if(response == null)
            {
                return NotFound();
            }

            if (response.IsSuccess || !String.IsNullOrEmpty(response.ErrorMessage))
            {
                return Ok(response);
            }

            return BadRequest();
        }

        /// <summary>
        /// Creates Cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<CreateCartResModel>> CreateCart(CreateCartReq request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _cartServices.CreateCart(request));
            }
            return BadRequest("Could not create cart");
        }

        /// <summary>
        /// Add an item into cart. If item exist in cart. Amount will increase 1. If not amount default is 1
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Result of add an item to the cart</returns>
        [HttpPost("add/item")]
        public async Task<IActionResult> AddItemToCart(AddItemToCartReqModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.AddItemToCart(request);
                if (response.IsSuccess)
                {
                    var cartConnectionIds = await _tracker.GetUserConnectionsOfCart(request.CartId);
                    if (cartConnectionIds != null)
                    {
                        await _cartHub.Clients.Clients(cartConnectionIds)
                            .SendAsync(HubMethodConstants.AddItemToCart, response);
                    }
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Remove an item from cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Result of remove an item from the cart</returns>
        [HttpPost("remove/item")]
        public async Task<IActionResult> RemoveItemFromCart(RemoveItemFromCartReqModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.RemoveItemFromCart(request);
                if (response.IsSuccess)
                {
                    var cartConnectionIds = await _tracker.GetUserConnectionsOfCart(request.CartId);
                    if (cartConnectionIds != null)
                    {
                        await _cartHub.Clients.Clients(cartConnectionIds)
                            .SendAsync(HubMethodConstants.RemoveItemFromCart, response);
                    }
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Submit items to cart. Send items of the customer for adding to the cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Result of submit items to the cart</returns>
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitItemsInCart(SubmitItemsInCartReqModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.SubmitItemsInCartAsync(request);
                if (response.IsSuccess) {
                    var cartConnectionIds = await _tracker.GetUserConnectionsOfCart(request.CartId);
                    if (cartConnectionIds != null)
                    {
                        var signalrResponse = new SubmitCartSignalrResponse
                        {
                            CartId = request.CartId,
                            CustomerId = request.CustomerId,
                            Items = request.Items
                        };

                        await _cartHub.Clients.Clients(cartConnectionIds)
                            .SendAsync(HubMethodConstants.SubmitItems, signalrResponse);
                    }              
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Unsubmit items in cart of the customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("unsubmit")]
        public async Task<IActionResult> UnSubmitItemsIncart(UnsubmitItemsReq request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.UnsubmitItems(request);
                if (response.IsSuccess) {
                    var cartConnectionIds = await _tracker.GetUserConnectionsOfCart(request.CartId);
                    if (cartConnectionIds != null)
                    {
                        var signalrResponse = new UnSubmitCartSignalrResponse
                        {
                            CustomerId = request.CustomerId,
                            CartId = request.CartId
                        };

                        await _cartHub.Clients.Clients(cartConnectionIds)
                            .SendAsync(HubMethodConstants.UnsubmitItems, signalrResponse);
                    }     
                }
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get exist cart that customer has created before with the shop
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("exist/shop/customer")]
        public async Task<IActionResult> GetExistCart(GetExistCartReqModel request)
        {

            if (ModelState.IsValid)
            {
                var response = await _cartServices.GetExistCartByShopIdAndCustomerId(request);
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Host removed co-making customer from cart if they want
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("remove/customer")]
        public async Task<IActionResult> RemoveCustomerFromCart(RemoveCustomerReqModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartServices.RemoveCustomerFromCart(request);
                if (response.IsSuccess)
                {
                    var cartConnectionIds = await _tracker.GetUserConnectionsOfCart(request.CartId);
                    if (cartConnectionIds != null)
                    {
                        var signalrResponse = new RemovedCustomerSignalrRes
                        {
                            CustomerIdRemoved = request.CustomerIdToRemoved,
                            CartId = request.CartId
                        };

                        await _cartHub.Clients.Clients(cartConnectionIds)
                            .SendAsync(HubMethodConstants.RemovedCustomer, signalrResponse);
                    }
                }
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
