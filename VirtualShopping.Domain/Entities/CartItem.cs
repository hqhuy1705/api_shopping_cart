using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualShopping.Domain.Entities
{
    public class CartItem
    {
        private int _id;
        private int _amount;
        private double _price;
        private bool _isDeleted;
        private bool _readyToOrder;

        public CartItem() {}

        [Key]
        [MaxLength(6)]
        public int Id { get => _id; set => _id = value; }
        [Required]
        [Range(0, 1000)]
        public int Amount { get => _amount; set => _amount = value; }

        public double Price { get => _price; set => _price = value; }

        [ForeignKey("CustomerId")]
        public string CustomerId { get ; set ; }
        public Customer Customer { get; set; }

        [ForeignKey("CartId")]
        public string CartId { get ; set; }
        public Cart Cart { get; set; }

        [ForeignKey("ItemId")]
        public string ItemId { get ; set ; }
        public Item Item { get; set; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }
        public bool ReadyToOrder { get => _readyToOrder; set => _readyToOrder = value; }
    }
}