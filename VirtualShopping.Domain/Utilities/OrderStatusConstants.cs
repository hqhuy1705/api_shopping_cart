using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Utilities
{
    public static class OrderStatusConstants
    {
        public const string Cancelled = "Cancelled";
        public const string Confirmed = "Confirmed";
        public const string SentToKitchen = "Sent To Kitchen";
        public const string ReadyForPickup = "Ready for Pickup";
        public const string Delivered = "Delivered";
    }
}
