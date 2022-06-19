using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShopping.Domain.Utilities
{
    public static class RegexConstants
    {
        public const string PhoneNumber = "^0+[0-9]{9}$";
        public const string Email = @"([\w])+([\w._])*\@([\w{2,}\-])+(\.[\w]{2,4})$";
    }
}
