using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhothoStock.Models
{
    public class PhotoInfo
    {
        [Key]
        public int  Id { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [ForeignKey("UserName")]
        public string UserName { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        public string PhotoName { get; set; }
        public List<Filter> Categories { get; set; }
    }
}
