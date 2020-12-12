using PhothoStock.Data.DTO;
using PhothoStock.Models;
using PhothoStock.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Utils
{
    public static class DTOExtensions
    {
        public static ApplicationUser ToApplicationUser(this SignUpData data)
        {
            return new()
            {
                Email = data.Email,
                PasswordHash = data.Password,
                UserName = data.Nickname
            };
        }
        public static bool NoFiltersSet(this List<Filter> filters)
        {
            foreach (var i in filters)
                if (i.Active)
                    return false;

            return true; 
        }
    }
}
