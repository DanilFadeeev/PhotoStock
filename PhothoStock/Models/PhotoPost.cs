using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Models
{
    public class PhotoPost
    {
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
