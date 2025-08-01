﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockPlaform.Dtos.Comment;
using StockPlaform.Dtos.Stock;
using StockPlaform.Extensions;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;
using StockPlaform.Models;

namespace StockPlaform.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo,
            UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }


            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);



        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound("comment not found");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create(int stockId, [FromBody] CreateCommentRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

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
            //get user from jwt token claims
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var newComment = createDto.ToCommentFromCreateDto(stockId);
            newComment.AppUserId = appUser.Id; // set the AppUserId from the authenticated user
            await _commentRepo.CreateAsync(newComment);
            return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment.ToCommentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            if (updateDto == null)
            {
                return BadRequest("Invalid comment data.");
            }
            var existingComment = await _commentRepo.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound("Comment not found");
            }

            existingComment.UpdateCommentFromDto(updateDto);

            var updatedComment = await _commentRepo.UpdateAsync(existingComment);
            return Ok(updatedComment.ToCommentDto());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            var commentToDelete = await _commentRepo.DeleteAsync(id);
            if (commentToDelete == null)
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }


      




    }
}