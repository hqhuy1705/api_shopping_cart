using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests;
using VirtualShopping.Domain.Requests.Cart;
using VirtualShopping.Domain.Requests.CartItem;
using VirtualShopping.Domain.Requests.Order;
using VirtualShopping.Domain.Responses.Cart;
using VirtualShopping.Domain.Responses.CartItem;
using VirtualShopping.Domain.Responses.Order;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.BLL.Implement
{
    public class CartServices : ICartServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartServices(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<SubmitItemsInCartResModel> SubmitItemsInCartAsync(SubmitItemsInCartReqModel request)
        {
            try
            {
                var existCart = await _unitOfWork.CartRepository.GetCartEntityById(request.CartId);

                if(existCart == null)
                {
                    return new SubmitItemsInCartResModel(ErrorConstants.NotFoundCart);
                }

                if(existCart.OrderTime.HasValue && !String.IsNullOrEmpty(existCart.DeliveryInformation))
                {
                    return new SubmitItemsInCartResModel(ErrorConstants.CannotChangedItemsInCart);
                }

                foreach (var item in request?.Items)
                {
                    CartItem existItemInCart = await _unitOfWork.CartItemRepository
                                            .GetItemFromCartAsync(request.CartId, item.ItemId, request.CustomerId);
                    Item existItem = await _unitOfWork.ItemRepository.GetItemByIDAsync(item.ItemId);

                    if (existItem == null)
                    {
                        return new SubmitItemsInCartResModel(ErrorConstants.NotFoundItem);
                    }

                    if (!existItem.IsActive && !item.IsDeleted && item.Amount > 0)
                    {
                        return new SubmitItemsInCartResModel(ErrorConstants.ExistItemsInactive);
                    }

                    if (existItem.ShopId != existCart.ShopId)
                    {
                        return new SubmitItemsInCartResModel(ErrorConstants.CannotSubmitCart);
                    }

                    if (existItemInCart == null)
                    {
                        await AddNewItemToCart(item, request, existItem);
                    }
                    else
                    {
                        UpdateExistItemInCart(existItemInCart, item, existItem);
                    }
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return new SubmitItemsInCartResModel();
                }

                return new SubmitItemsInCartResModel(ErrorConstants.CannotSubmitCart);
            }
            catch (Exception ex)
            {
                return new SubmitItemsInCartResModel(ErrorConstants.UnknownError);
            }
        }

        public async Task<GetCartResModel> GetCartById(string cartId, bool getShop)
        {
            var cart = await _unitOfWork.CartRepository.GetCartForClientById(cartId, getShop);
            if(cart == null)
            {
                var existOrder = await _unitOfWork.CartRepository.GetOrderEntityById(cartId);
                if (existOrder != null)
                {
                    return new GetCartResModel
                    {
                        CartId = existOrder.CartId,
                        ErrorMessage = ErrorConstants.CartChangeToOrder
                    };
                }
            }

            return cart;
        }

        // this can be used to get all orders too since it just returns a list of carts in viewModel
        public async Task<GetAllOrdersResModel> GetAllOrdersByCustomerId(string customerId)
        {

            var orders = await _unitOfWork.CartRepository.GetAllOrdersByCustomerId(customerId);
            return orders != null
            ? new GetAllOrdersResModel
            {
                Orders = orders
            }
            : new GetAllOrdersResModel
            {
                ErrorMessage = "Could not get orders"
            };
        }
        public async Task<GetOrderResModel> GetOrderById(string orderId)
        {
            return await _unitOfWork.CartRepository.GetOrderForClientById(orderId);
        }

        public async Task<CreateCartResModel> CreateCart(CreateCartReq request)
        {
            var existCart = await _unitOfWork.CartRepository.GetExistCartByShopIdAndCustomerId(request.ShopId, request.CustomerId);

            if (existCart != null)
            {
                return new CreateCartResModel
                {
                    CartId = existCart.CartId,
                    ErrorMessage = ErrorConstants.ExistCartAvailable
                };
            }
            var newCart = new Cart
            {
                CartId = Helper.IdGenerator(),
                ShopId = request.ShopId,
                CustomerId = request.CustomerId
            };

            await HandleNewCartIsUnique(newCart);

            try
            {
                var result = await _unitOfWork.CartRepository.CreateCart(newCart);
                if (result)
                    return new CreateCartResModel
                    {
                        CartId = newCart.CartId
                    };

                return new CreateCartResModel
                {
                    ErrorMessage = "Failed to create cart"
                };
            }
            catch (Exception ex)
            {
                return new CreateCartResModel
                {
                    ErrorMessage = ErrorConstants.UnknownError
                };
            }
        }

        public async Task<PlacedNewOrderResModel> PlacedNewOrder(PlacedNewOrderReq request)
        {
            var existCart = await _unitOfWork.CartRepository.GetCartEntityById(request.CartId);
            if (existCart == null
                || (existCart != null && !String.IsNullOrEmpty(existCart?.DeliveryInformation) && existCart.OrderTime.HasValue))
            {
                return new PlacedNewOrderResModel
                {
                    ErrorMessage = ErrorConstants.NotFoundCart
                };
            }

            var itemsInCart = await _unitOfWork.CartItemRepository.GetItemsFromCartByCartIdAsync(existCart.CartId);

            if (itemsInCart.Count == 0)
            {
                return new PlacedNewOrderResModel
                {
                    ErrorMessage = ErrorConstants.EmptyCart
                };
            }

            bool existItemsInActive = itemsInCart.Exists(i => !i.IsDeleted && !i.Item.IsActive);

            if (existItemsInActive)
            {
                return new PlacedNewOrderResModel
                {
                    ErrorMessage = ErrorConstants.ExistItemsInactive
                };
            }

            bool isNotAllComakingReady = itemsInCart.Exists(i => !i.IsDeleted && i.Item.IsActive && i.CustomerId != existCart.CustomerId && !i.ReadyToOrder);

            if (isNotAllComakingReady)
            {
                return new PlacedNewOrderResModel
                {
                    ErrorMessage = ErrorConstants.YoursCoMakingMustReadyForOrder
                };
            }

            return await _unitOfWork.CartRepository.PlacedNewOrder(request, existCart)
            ? new PlacedNewOrderResModel
            {
                OrderId = request.CartId,
                ShopId = existCart.ShopId,
                CustomerId = existCart.CustomerId,
                CustomerName = existCart.Customer.Name,
                CustomerPhoneNumber = existCart.Customer.PhoneNumber,
                OrderTime = existCart.OrderTime.Value,
                Status = existCart.Status,
                TotalPrice = CalculateTotalPrice(itemsInCart),
                ItemsInCart = itemsInCart.Select(i => new ItemInCartViewModel
                {
                    Amount = i.Amount,
                    CartId = i.CartId,
                    CustomerId = i.CustomerId,
                    IsDeleted = i.IsDeleted,
                    ItemId = i.ItemId,
                    Price = i.Price,
                    ReadyToOrder = i.ReadyToOrder,
                    CustomerName = i.Customer.Name,
                    ItemName = i.Item.Name,
                    ItemIsActive = i.Item.IsActive
                }),
                DeliveryInformation = existCart.DeliveryInformation
            }
            : new PlacedNewOrderResModel
            {
                ErrorMessage = "Failed to place new order"
            };

        }

        public async Task<CancelOrderResModel> CancelOrder(CancelOrderReq request)
        {
            ChangeOrderStatusReqModel mappingRequest = new ChangeOrderStatusReqModel
            {
                OrderId = request.OrderId,
                OrderStatus = OrderStatusConstants.Cancelled,
                CustomerId = request.CustomerId
            };

            var result = await ChangeOrderStatus(mappingRequest);
            if (result.IsSuccess)
            {
                return new CancelOrderResModel
                {
                    OrderId = result.OrderId,
                    ShopId = result.ShopId
                };
            }

            return new CancelOrderResModel(result.ErrorMessage);
        }

        public async Task<GetAllOrdersResModel> GetAllOrdersByShopId(string shopId)
        {

            var orders = await _unitOfWork.CartRepository.GetAllOrdersByShopId(shopId);
            return orders != null
            ? new GetAllOrdersResModel
            {
                Orders = orders
            }
            : new GetAllOrdersResModel
            {
                ErrorMessage = ErrorConstants.NotFoundOrder
            };
        }

        public async Task<ChangeOrderStatusResModel> ChangeOrderStatus(ChangeOrderStatusReqModel request)
        {
            var existOrder = await _unitOfWork.CartRepository.GetOrderEntityById(request.OrderId);

            var errorValidate = HandleErrorValidate(existOrder, request);

            if (errorValidate != null)
            {
                return errorValidate;
            }

            var existOrderStatusEnum = Helper.MappingOrderStatusToEnum(existOrder.Status);
            var newOrderStatusEnum = Helper.MappingOrderStatusToEnum(request.OrderStatus);

            var error = HandleChangeOrderStatusError(existOrderStatusEnum, newOrderStatusEnum);

            if (error != null)
            {
                return error;
            }

            try
            {
                var result = await _unitOfWork.CartRepository.UpdateOrderStatus(existOrder, request.OrderStatus);
                if (result)
                {
                    return new ChangeOrderStatusResModel
                    {
                        OrderId = existOrder.CartId,
                        NewStatus = existOrder.Status,
                        ShopId = existOrder.ShopId
                    };
                }

                return new ChangeOrderStatusResModel
                {
                    ErrorMessage = ErrorConstants.UnknownError
                };
            }
            catch (Exception ex)
            {
                return new ChangeOrderStatusResModel
                {
                    ErrorMessage = ErrorConstants.UnknownError
                };
            }
        }

        public async Task<UnsubmitItemsResModel> UnsubmitItems (UnsubmitItemsReq request) {

            var cartItemList = await _unitOfWork.CartRepository.GetCustomerAllItems(
                new GetCustomerAllItemReq {
                    CustomerId = request.CustomerId,
                    CartId = request.CartId
                }
            );

            if (cartItemList == null) {

                return new UnsubmitItemsResModel(ErrorConstants.CannotFindItems);
            }

            foreach (var cartItem in cartItemList) {

                if (!await _unitOfWork.CartItemRepository.ToggleItemReadyAsync(cartItem.Id)) {
                    
                    return new UnsubmitItemsResModel(ErrorConstants.NotFoundItem);
                }
            }

            var result = await _unitOfWork.SaveChangesAsync();
            return (result > 0)
            ? new UnsubmitItemsResModel()
            : new UnsubmitItemsResModel(ErrorConstants.YourStatusOfCartNotChanged);
        }

        public async Task<GetCartResModel> GetExistCartByShopIdAndCustomerId(GetExistCartReqModel request)
        {
            return await _unitOfWork.CartRepository.GetExistCartByShopIdAndCustomerId(request.ShopId, request.CustomerId);
        }

        public async Task<RemoveCustomerResModel> RemoveCustomerFromCart(RemoveCustomerReqModel request)
        {
            var existCart = await _unitOfWork.CartRepository.GetCartEntityById(request.CartId);
            if (existCart == null || (existCart != null && String.IsNullOrEmpty(existCart.DeliveryInformation) && existCart.OrderTime.HasValue))
            {
                return new RemoveCustomerResModel
                {
                    ErrorMessage = ErrorConstants.NotFoundCart
                };
            }

            if (existCart.CustomerId != request.CustomerId)
            {
                return new RemoveCustomerResModel
                {
                    ErrorMessage = ErrorConstants.NotYourCart
                };
            }

            var itemsOfCustomerRemoved = existCart.ItemsInCart.Where(i => !i.IsDeleted && i.CustomerId == request.CustomerIdToRemoved);
            if (!itemsOfCustomerRemoved.Any())
            {
                return new RemoveCustomerResModel
                {
                    ErrorMessage = ErrorConstants.CustomerNotExistInCart
                };
            }

            foreach(var item in itemsOfCustomerRemoved)
            {
                item.IsDeleted = true;
            }

            var result = (await _unitOfWork.SaveChangesAsync() > 0);
            if (result)
            {
                return new RemoveCustomerResModel
                {
                    CartId = request.CartId,
                    CustomerIdToRemoved = request.CustomerIdToRemoved
                };
            }

            return new RemoveCustomerResModel
            {
                ErrorMessage = ErrorConstants.UnknownError
            };
        }

        public async Task<AddItemToCartResModel> AddItemToCart(AddItemToCartReqModel request)
        {
            var existCart = await _unitOfWork.CartRepository.GetCartEntityById(request.CartId);

            if (existCart == null || (existCart != null && String.IsNullOrEmpty(existCart.DeliveryInformation) && existCart.OrderTime.HasValue))
            {
                return new AddItemToCartResModel
                {
                    ErrorMessage = ErrorConstants.NotFoundCart
                };
            }

            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.CustomerId);
            if(customer == null)
            {
                return new AddItemToCartResModel
                {
                    ErrorMessage = ErrorConstants.NotFoundCustomer
                };
            }

            var itemsInCartOfCustomer = existCart.ItemsInCart.Where(i => i.CustomerId == request.CustomerId && !i.IsDeleted).ToList();
            var isCustomerSubmited = itemsInCartOfCustomer?.Count > 0 && itemsInCartOfCustomer.All(i => i.ReadyToOrder);

            if (isCustomerSubmited)
            {
                return new AddItemToCartResModel
                {
                    ErrorMessage = ErrorConstants.CannotChangedItemsInCart
                };
            }

            var existItemInCart = existCart.ItemsInCart.Where(i => i.ItemId == request.ItemId && i.CustomerId == request.CustomerId && !i.IsDeleted)
                                                        .FirstOrDefault();
            if(existItemInCart != null)
            {
                existItemInCart.Amount++;
            }

            if(existItemInCart == null)
            {
                var item = await _unitOfWork.ItemRepository.GetItemByIDAsync(request.ItemId);

                if(item == null)
                {
                    return new AddItemToCartResModel
                    {
                        ErrorMessage = ErrorConstants.NotFoundItem
                    };
                }

                if (item.ShopId != existCart.ShopId)
                {
                    return new AddItemToCartResModel
                    {
                        ErrorMessage = ErrorConstants.CannotAddItemsOfOtherShop
                    };
                }

                var newItemInCart = new CartItem
                {
                    Amount = 1,
                    CartId = request.CartId,
                    CustomerId = request.CustomerId,
                    IsDeleted = false,
                    ReadyToOrder = false,
                    ItemId = request.ItemId,
                    Price = item.Price
                };

                await _unitOfWork.CartItemRepository.AddNewItemIntoCartAsync(newItemInCart);
            }

            var result = (await _unitOfWork.SaveChangesAsync()) > 0;

            if (result)
            {
                return new AddItemToCartResModel
                {
                    CartId = request.CartId,
                    CustomerId = request.CustomerId,
                    CustomerName = customer.Name,
                    ItemId = request.ItemId,
                    Amount = existItemInCart != null ? existItemInCart.Amount : 1
                };
            }
            return new AddItemToCartResModel
            {
                ErrorMessage = ErrorConstants.UnknownError
            };
        }

        public async Task<RemoveItemFromCartResModel> RemoveItemFromCart(RemoveItemFromCartReqModel request)
        {
            var item = await _unitOfWork.ItemRepository.GetItemByIDAsync(request.ItemId);
            if(item == null)
            {
                return new RemoveItemFromCartResModel(ErrorConstants.NotFoundItem);
            }

            var cart = await _unitOfWork.CartRepository.GetCartEntityById(request.CartId);
            if(cart == null)
            {
                return new RemoveItemFromCartResModel(ErrorConstants.NotFoundCart);
            }

            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return new RemoveItemFromCartResModel(ErrorConstants.NotFoundCustomer);
            }

            var existItemInCart = await _unitOfWork.CartItemRepository.GetItemFromCartAsync(request.CartId, request.ItemId, request.CustomerId);
            if(existItemInCart == null)
            {
                return new RemoveItemFromCartResModel(ErrorConstants.ItemNotExistInCart);
            }

            existItemInCart.IsDeleted = true;
            var result = (await _unitOfWork.SaveChangesAsync()) > 0;
            if (result)
            {
                return new RemoveItemFromCartResModel
                {
                    CartId = request.CartId,
                    CustomerId = request.CustomerId,
                    ItemId = request.ItemId
                };
            }

            return new RemoveItemFromCartResModel(ErrorConstants.UnknownError);
        }

        #region Private Method
        private async Task HandleNewCartIsUnique(Cart newCart)
        {
            var existCart = await _unitOfWork.CartRepository.GetCartEntityById(newCart.CartId);

            while (existCart != null)
            {
                newCart.CartId = Helper.IdGenerator();
                existCart = await _unitOfWork.CartRepository.GetCartEntityById(newCart.CartId);
            }
        }

        private async Task AddNewItemToCart(ItemInCartModel item, SubmitItemsInCartReqModel request, Item existItem)
        {
            if (item.Amount == 0) return;

            var newItem = new CartItem
            {
                Amount = item.Amount,
                Price = existItem.Price,
                CartId = request.CartId,
                CustomerId = request.CustomerId,
                ItemId = item.ItemId,
                IsDeleted = false,
                ReadyToOrder = true
            };
            await _unitOfWork.CartItemRepository.AddNewItemIntoCartAsync(newItem);
        }

        private void UpdateExistItemInCart(CartItem existItemInCart, ItemInCartModel item, Item existItem)
        {
            existItemInCart.Amount = item.Amount;
            existItemInCart.Price = existItem.Price;
            existItemInCart.IsDeleted = item.IsDeleted || item.Amount <= 0;
            existItemInCart.ReadyToOrder = true;
        }

        private ChangeOrderStatusResModel HandleChangeOrderStatusError
                                (OrderStatusEnum existOrderStatusEnum, OrderStatusEnum newOrderStatusEnum)
        {
            if (existOrderStatusEnum == OrderStatusEnum.Cancelled)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.OrderWasCancelled);
            }

            if (existOrderStatusEnum == OrderStatusEnum.Delivered)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.OrderIsFinished);
            }

            if (existOrderStatusEnum >= OrderStatusEnum.Confirmed && newOrderStatusEnum == OrderStatusEnum.Cancelled)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.OrderCannotCancel);
            }

            if (existOrderStatusEnum >= OrderStatusEnum.Confirmed && newOrderStatusEnum < existOrderStatusEnum)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.CannotChangedStatusToPrevious);
            }

            return null;
        }

        private ChangeOrderStatusResModel HandleErrorValidate(Cart existOrder, ChangeOrderStatusReqModel request)
        {
            if (existOrder == null)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.NotFoundOrder);
            }

            if (existOrder.CustomerId != request.CustomerId
                    && !String.IsNullOrEmpty(request.CustomerId)
                    && String.IsNullOrEmpty(request.ShopId))
            {
                return new ChangeOrderStatusResModel(ErrorConstants.CannotModifyOrderStatusOfOtherCustomer);
            }

            if (existOrder.CustomerId == request.CustomerId
                    && !String.IsNullOrEmpty(request.CustomerId)
                    && String.IsNullOrEmpty(request.ShopId)
                    && request.OrderStatus != OrderStatusConstants.Cancelled
                    && request.OrderStatus != OrderStatusConstants.Delivered)
            {
                return new ChangeOrderStatusResModel(ErrorConstants.CustomerNotAllowToChangeThisStatus);
            }

            if (existOrder.ShopId != request.ShopId
                    && !String.IsNullOrEmpty(request.ShopId)
                    && String.IsNullOrEmpty(request.CustomerId))
            {
                return new ChangeOrderStatusResModel(ErrorConstants.CannotModifyOrderStatusOfOtherShop);
            }

            return null;
        }

        private double CalculateTotalPrice(ICollection<CartItem> itemsInCart)
        {
            double totalPrice = 0;
            foreach (var item in itemsInCart)
            {
                totalPrice += item.Price * item.Amount;
            }
            return totalPrice;
        }
        #endregion
    }
}