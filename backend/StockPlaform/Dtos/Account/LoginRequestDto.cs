using System.ComponentModel.DataAnnotations;

namespace StockPlaform.Dtos.Account
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
