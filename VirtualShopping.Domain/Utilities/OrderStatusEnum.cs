using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Utilities
{
    public enum OrderStatusEnum
    {
        Blank           = -1,
        Cancelled       = 0,
        Confirmed       = 1,
        SentToKitChen   = 2,
        ReadyForPickup  = 3,
        Delivered       = 4
    }
}
