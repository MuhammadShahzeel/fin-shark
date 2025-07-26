using System.ComponentModel.DataAnnotations;

namespace StockPlaform.Dtos.Account
{
    public class RegisterRequestDto
    {
        
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
