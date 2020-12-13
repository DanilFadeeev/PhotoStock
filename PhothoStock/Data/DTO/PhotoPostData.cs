using Microsoft.AspNetCore.Http;
using PhothoStock.Models;
using System.Collections.Generic;

namespace PhothoStock.Data.DTO
{
    public class PhotoPostData
    {
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
