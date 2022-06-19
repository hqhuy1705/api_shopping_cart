using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Utilities
{
    public static class ErrorConstants
    {
        //Shop Error
        public const string NotFoundShop = "Shop does not exists";
        public const string InvalidPN = "PhoneNumber is invalid.";
        public const string DuplicatePN = "PhoneNumber already registered.";
        public const string InvalidForm = "Error Unkown";
        public const string UnknownError = "Unknown error, please contact Administrator";

        //Cart Error
        public const string NotFoundCart = "Cart does not exist";
        public const string CannotChangedItemsInCart = "Cannot changed items in cart. You already placed order by this cart";
        public const string FailedToUpdateCart = "Falied to update items in cart. Your items in cart not changed";
        public const string CannotSubmitCart = "Could not submit cart. Your cart items are still unsubmitted";
        public const string CannotFindItems = "Could not find at least one of your items, please try again";
        public const string CannotUnsubmitCart = "Could not unsubmit cart. Your cart items are still submitted";
        public const string ExistCartAvailable = "You have a exit cart with this shop";
        public const string EmptyCart = "Your cat is empty";
        public const string CustomerNotExistInCart = "Customer you want to removed not exist in cart";
        public const string NotYourCart = "This is not your cart";
        public const string CannotAddItemsOfOtherShop = "Cannot add items of other shop to this cart";
        public const string CartChangeToOrder = "This cart not exist anymore. You has an order from this cart";
        public const string YourStatusOfCartNotChanged = "Your cart status not changed";

        //Order Error
        public const string NotFoundOrder = "Order does not exist";
        public const string OrderWasCancelled = "Cannot changed order status. Order was closed before";
        public const string OrderCannotCancel = "Order was confirmed or ready for delivery then you cannot cancel this order";
        public const string CannotChangedStatusToPrevious = "You cannot changed order status to previous one";
        public const string CannotModifyOrderStatusOfOtherCustomer = "This order is not yours. You cannot change order status";
        public const string CannotModifyOrderStatusOfOtherShop = "This order is not yours. You cannot change order status";
        public const string CustomerNotAllowToChangeThisStatus = "Customer not allow to change this status for order";
        public const string YoursCoMakingMustReadyForOrder = "Your Co-making not ready for place new order";
        public const string ExistItemsInactive = "Your cart exist items inactive, please reload your cart";
        public const string OrderIsFinished = "Order was delivered";

        //Customer Errors
        public const string PhoneNumberIsUsed = "Your phone number is used by another account, please input another phone number and try again";
        public const string NotFoundCustomer = "Customer does not exist";

        //Item Errors
        public const string NotFoundItem = "Item not found";
        public const string NotYourItem = "This item not belong to your shop";
        public const string ItemNotExistInCart = "This item exist in this cart";

    }
}
