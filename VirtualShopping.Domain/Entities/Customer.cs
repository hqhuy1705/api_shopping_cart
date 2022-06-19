using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Domain.Entities
{
    public class Customer
    {
        private string _customerId;
        private string _name;
        private string _phoneNumber;
        private string _avatar;

        [Key]
        [MaxLength(6)]
        public string CustomerId { get => _customerId; set => _customerId = value; }
        [Required]
        [MaxLength(100)]
        public string Name { get => _name; set => _name = value; }
        [MaxLength(15)]
        [Required]
        [RegularExpression(RegexConstants.PhoneNumber)]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Avatar { get => _avatar; set => _avatar = value; }
        public bool IsActived { get; set; }
    }
}
