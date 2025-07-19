using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPlaform.Data;
using StockPlaform.Dtos.Stock;
using StockPlaform.Mappers;
using System.Collections.Immutable;

namespace StockPlaform.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s=>s.ToStockDto());
       
            return Ok(stocks);


        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null) {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (stockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var stockModel = stockDto.ToStockFromCreateDto(); //dto->model
            _context.Stocks.Add(stockModel);//model->db
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }










    }
}
