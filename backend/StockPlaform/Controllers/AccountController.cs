using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPlaform.Dtos.Account;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;
using StockPlaform.Models;

namespace StockPlaform.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;       // User create/update/delete
        private readonly ITokenService _tokenService;             // JWT token generate karta hai
        private readonly SignInManager<AppUser> _signInManager;   // Login aur password check karta hai

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        // ============================
        // ======= REGISTER USER ======
        // ============================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState); // Input validation fail ho gayi

                var appUser = registerDto.ToAppUser(); // DTO ko AppUser mein convert kiya

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password); // User create kiya

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // "User" role assign kiya

                    if (roleResult.Succeeded)
                    {
                        var token = _tokenService.CreateToken(appUser); // JWT token banaya
                        var newUserDto = appUser.ToNewUserDto(token);   // DTO banaya response ke liye
                        return Ok(newUserDto);                          // 200 OK response with token
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors); // Role assign mein error
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors); // User create nahi ho saka
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e); // Unexpected error handle
            }
        }

        // ============================
        // ========= LOGIN USER =======
        // ============================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState); // Input validation fail ho gayi

                // Username ke zariye user find karo
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

                if (user == null)
                    return Unauthorized("Invalid username or password"); // User exist nahi karta

                // Password check karo Identity system se
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                /*
                   is line ka deep meaning:
                   - yeh method verify karta hai ke password sahi hai ya nahi
                   - user ka jo password frontend se aaya (plain text) wo
                     database ke andar stored hashed password ke sath match kiya jata hai
                   - agar match ho gaya to result.Succeeded = true
                   - agar nahi match hua to result.Succeeded = false
                   - 3rd parameter "false" ka matlab: lockoutOnFailure = false
                     → agar user galat password de to lockout attempt count na karo
                */

                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid username or password"); // Password match nahi hua
                }

                var token = _tokenService.CreateToken(user); // Token generate karo

                var userDto = user.ToLoginUserDto(token);    // DTO banayo login response ke liye
                return Ok(userDto);                          // 200 OK with token
            }
            catch (Exception e)
            {
                return StatusCode(500, e); // Koi bhi runtime error
            }
        }
    }
}
