using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockPlaform.Extensions;
using StockPlaform.Interfaces;
using StockPlaform.Models;

namespace StockPlaform.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(

            UserManager<AppUser> userManager,
            IStockRepository stockRepository,
            IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {

            //  JWT token me se username nikalna using custom ClaimsExtension method
            var username = User.GetUsername();

            //  Identity system ka UserManager use karke user ko database se find karna
            var appUser = await _userManager.FindByNameAsync(username);

            //FindByNameAsync yeh method database me AspNetUsers table (ya jo Identity ke users ka table ho) me UserName = 'shahzeel123' ko search karega.

            var UserPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            return Ok(UserPortfolio);


        }

    }
}
