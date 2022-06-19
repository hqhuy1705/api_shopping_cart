using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Item
{
    public class ItemDTO
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
        public string ShopId { get; set; }
    }
}
