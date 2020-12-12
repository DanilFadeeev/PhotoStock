using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Models
{
    public class PhotoInfoAndFilters
    {
       public List<PhotoInfo> PhotosInfo { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
