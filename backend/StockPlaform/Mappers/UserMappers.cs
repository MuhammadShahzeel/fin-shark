using StockPlaform.Dtos.Account;
using StockPlaform.Models;

namespace StockPlaform.Mappers
{
    public static class UserMappers
    {



        public static AppUser ToAppUser(this RegisterRequestDto dto)
        {
            return new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email
                // dont use Password here, it will be set by UserManager in controller because it hased and salted automatically
            };
        }
        public static NewUserDto ToNewUserDto(this AppUser user, string token)
        {
            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
