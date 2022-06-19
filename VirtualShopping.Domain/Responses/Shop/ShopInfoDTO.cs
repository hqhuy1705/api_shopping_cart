using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Responses.Shop
{
    public class ShopInfoDTO
    {
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public ICollection<VirtualShopping.Domain.Entities.Item> Items { get; set; }
        public string ErrorMessage { get; set; }
    }
}
