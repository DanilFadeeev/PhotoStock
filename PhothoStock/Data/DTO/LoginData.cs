using System.ComponentModel.DataAnnotations;

namespace PhothoStock.Data.DTO
{
    public class LoginData
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
