using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Image { get; set; }
        public string About { get; set; }
        public List<PhotoInfo> Photos { get; set; }
        [NotMapped]
        public bool IsEnabled => LockoutEnd is null || LockoutEnabled && LockoutEnd < new DateTimeOffset(DateTime.Now);
        
    }
}
