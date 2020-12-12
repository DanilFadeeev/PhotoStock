using Microsoft.AspNetCore.Http;
using PhothoStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Data.DTO
{
    public class PhotoPostData
    {
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
