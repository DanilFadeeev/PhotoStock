using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Models
{
    public class Filter
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }
    }
}
