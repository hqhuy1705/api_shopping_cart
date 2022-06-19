using System.ComponentModel.DataAnnotations;

namespace VirtualShopping.Domain.Requests.Cart
{
    public class UnsubmitItemsReq
    {
        [Required]
        [MaxLength(6)]
        public string CustomerId { get; set; }
        [Required]
        [MaxLength(6)]
        public string CartId { get; set; }
    }
}