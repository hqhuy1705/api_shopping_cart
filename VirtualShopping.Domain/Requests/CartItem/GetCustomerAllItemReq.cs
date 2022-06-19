namespace VirtualShopping.Domain.Requests.CartItem
{
    public class GetCustomerAllItemReq
    {
        public string CustomerId { get; set; } 
        public string CartId { get; set; }
    }
}