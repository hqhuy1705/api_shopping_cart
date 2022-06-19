using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VirtualShopping.Domain.Entities
{
    public class Cart
    {
        private string _cartId;
        private string _status;
        private DateTime? _orderTime;
        private DateTime? _deliveryTime;
        private string _deliveryInformation;

        public Cart() {}
      
        [Key]
        [MaxLength(6)]
        public string CartId { get => _cartId; set => _cartId = value; }


        public string Status { get => _status; set => _status = value; }

        public DateTime? OrderTime { get => _orderTime; set => _orderTime = value; }

        public DateTime? DeliveryTime { get => _deliveryTime; set => _deliveryTime = value; }

        public string DeliveryInformation { get => _deliveryInformation; set => _deliveryInformation = value; }

        [ForeignKey("ShopId")]
        public string ShopId { get; set; }
        public Shop Shop { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerId { get ; set ; }
        public Customer Customer { get; set; }
        public ICollection<CartItem> ItemsInCart { get; set; }
    }
}