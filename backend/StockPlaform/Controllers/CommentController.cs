using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPlaform.Interfaces;

namespace StockPlaform.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            var comments = await _commentRepo.GetAllAsync(); // 
            return Ok(comments);



        }
    }
}