using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPlaform.Dtos.Comment;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;

namespace StockPlaform.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);



        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create(int stockId, CreateCommentRequestDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid comment data.");
            }
            // Check if the stock exists
            var stockExists = await _stockRepo.ExistsAsync(stockId);
            if (!stockExists)
            {
                return NotFound("Stock not found.");
            }
            var newComment = createDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(newComment);
            return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment.ToCommentDto());








        }
    }
}