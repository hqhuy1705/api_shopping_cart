using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Utilities
{
    public static class Helper
    {
        public const string idTimeZoneUtc7 = "SE Asia Standard Time";
        private static DateTime baseDate = new DateTime(1970, 01, 01);

        public static async Task<string> ImgConvertor(IFormFile image)
        {
            var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            return Convert.ToBase64String(fileBytes);
        }

        public static string IdGenerator()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

        public static DateTime ConvertUTCToTimeZone(DateTime utcTime, string idTimeZone)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(idTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, cstZone);
        }

        public static string MappingOrderStatusToString(OrderStatusEnum status)
        {
            string strStatus = String.Empty;
            switch (status)
            {
                case OrderStatusEnum.Cancelled:
                    strStatus = OrderStatusConstants.Cancelled;
                    break;
                case OrderStatusEnum.Confirmed:
                    strStatus = OrderStatusConstants.Confirmed;
                    break;
                case OrderStatusEnum.SentToKitChen:
                    strStatus = OrderStatusConstants.SentToKitchen;
                    break;
                case OrderStatusEnum.ReadyForPickup:
                    strStatus = OrderStatusConstants.ReadyForPickup;
                    break;
                case OrderStatusEnum.Delivered:
                    strStatus = OrderStatusConstants.Delivered;
                    break;
                default:
                    strStatus = null;
                    break;
            }

            return strStatus;
        }

        public static OrderStatusEnum MappingOrderStatusToEnum(string status)
        {
            OrderStatusEnum orderStatusEnum = OrderStatusEnum.Blank;
            if (String.IsNullOrEmpty(status)){
                return orderStatusEnum;
            }
            switch (status)
            {
                case OrderStatusConstants.Cancelled:
                    orderStatusEnum = OrderStatusEnum.Cancelled;
                    break;
                case OrderStatusConstants.Confirmed:
                    orderStatusEnum = OrderStatusEnum.Confirmed;
                    break;
                case OrderStatusConstants.SentToKitchen:
                    orderStatusEnum = OrderStatusEnum.SentToKitChen;
                    break;
                case OrderStatusConstants.ReadyForPickup:
                    orderStatusEnum = OrderStatusEnum.ReadyForPickup;
                    break;
                case OrderStatusConstants.Delivered:
                    orderStatusEnum = OrderStatusEnum.Delivered;
                    break;
            }

            return orderStatusEnum;
        }
    }
}
