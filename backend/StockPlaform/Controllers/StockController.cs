using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Dtos.Stock;
using StockPlaform.Helpers;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;
using System.Collections.Immutable;

namespace StockPlaform.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            var stocks = await _stockRepo.GetAllAsync(query);


            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound("stock not found");
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }

            if (createDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var newStock = createDto.ToStockFromCreateDto(); //dto->model
            await _stockRepo.CreateAsync(newStock);//model->db

            return CreatedAtAction(nameof(GetById), new { id = newStock.Id }, newStock.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }
            if (updateDto == null)
                return BadRequest("Invalid stock data.");

            var existingStock = await _stockRepo.GetByIdAsync(id);
            if (existingStock == null)
                return NotFound("Stock not found");

            existingStock.UpdateStockFromDto(updateDto);

            var updatedStock = await _stockRepo.UpdateAsync(existingStock);
            return Ok(updatedStock.ToStockDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // to get all validation errors we provided in dtos
            }


            var stockToDelete = await _stockRepo.DeleteAsync(id);
            if (stockToDelete == null)
            {
                return NotFound("stock not found");
            }
            return NoContent();
        }













    }
}
