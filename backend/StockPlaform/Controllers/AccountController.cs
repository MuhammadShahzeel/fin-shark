// Required libraries import ki ja rahi hain jo Identity, MVC, DTOs, Models etc. handle karti hain
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPlaform.Dtos.Account;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;        
using StockPlaform.Models;       
using System.ComponentModel.DataAnnotations;

namespace StockPlaform.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Identity ka UserManager<AppUser> inject ho raha hai – yeh object users create/update/delete waghera karta hai
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        // Constructor injection – UserManager ko controller ke andar available banaya gaya
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        // POST endpoint: /api/account/register – yeh new user register karega
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            try
            {
                // 1. Check kar rahe hain ke jo data frontend sy aya hai wo valid hai ya nahi (ModelState)
                if (!ModelState.IsValid)
                {
                    // Agar koi validation error hai (e.g. required fields missing), to BadRequest return kar do
                    return BadRequest(ModelState);
                }

                // 2. DTO ko AppUser object mein convert kar rahe hain custom mapper function ke zariye (ToAppUser())
                var appUser = registerDto.ToAppUser();

                // 3. Identity ke method sy user ko database mein create kar rahe hain, password ke sath
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                // 4. Agar user successfully create ho gaya
                if (createdUser.Succeeded)
                {
                    // Us user ko "User" role assign kar rahe hain
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    // Agar role bhi assign ho gaya to success message bhej do
                    if (roleResult.Succeeded)
                    {
                        // Generate JWT token for the new user
                        var token = _tokenService.CreateToken(appUser);

                        // Use mapper to convert to NewUserDto
                        var newUserDto = appUser.ToNewUserDto(token);

                        return Ok(newUserDto);
                    }
                    else
                    {
                        // Agar role assign karte waqt koi error aya to 500 Internal Server Error ke sath error details return karo
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    // Agar user creation fail ho gaya to error detail return karo
                    return StatusCode(500, createdUser.Errors);
                }

                // ---------- Yeh neeche wali comments general flow ko describe kar rahi hain ----------
                // 1. Identity internally username/email validate karta hai
                // 2. Password rules check karta hai (jo tum program.cs mein set karte ho)
                // 3. Password ko hash karta hai (secure storage ke liye)
                // 4. DB mein user insert karta hai
                // 5. Agar success ho to appUser.Succeeded true hota hai
                // 6. Agar koi error ho to appUser.Errors mein detail milti hai
            }
            catch (Exception e)
            {
                // Agar koi unexpected error aa jaye (runtime error), to usko handle karo aur 500 return karo
                return StatusCode(500, e);
            }
        }


        // POST endpoint: /api/account/login – yeh user ko login karega
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
                if (user == null)
                    return Unauthorized("Invalid username or password");
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid username or password");
                }
                var token = _tokenService.CreateToken(user);

                // Use mapper for login response
                var userDto = user.ToLoginUserDto(token);
                return Ok(userDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
