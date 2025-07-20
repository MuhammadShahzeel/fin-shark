using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Dtos.Stock;
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

        public StockController(ApplicationDBContext context,IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();


            var stockDto = stocks.Select(s=>s.ToStockDto());
       
            return Ok(stockDto);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async  Task<IActionResult> Create([FromBody] CreateStockRequestDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var newStock = createDto.ToStockFromCreateDto(); //dto->model
            await _context.Stocks.AddAsync(newStock);//model->db
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newStock.Id }, newStock.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateDto)
        {

            if (updateDto == null)
            {
                return BadRequest("Invalid stock data.");
            }


            var stockToUpdate = await _context.Stocks.FindAsync(id); // its recommended but work only for PK
            //you can also use 
            //var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockToUpdate == null)
            {
                return NotFound();
            }
            stockToUpdate.UpdateFromDto(updateDto);
            await _context.SaveChangesAsync();

            return Ok(stockToUpdate.ToStockDto());





        }
        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(int id)
        {
            //var stockToDelete = _context.Stocks.FirstOrDefault(s => s.Id == id);
            var stockToDelete = await _context.Stocks.FindAsync(id); 
            if (stockToDelete is null)
                return NotFound();
            _context.Stocks.Remove (stockToDelete); //dont need await
            await _context.SaveChangesAsync();
            return NoContent(); 
        }












    }
}
