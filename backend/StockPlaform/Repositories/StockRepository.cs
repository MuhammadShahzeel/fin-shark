using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Interfaces;
using StockPlaform.Models;

namespace StockPlaform.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        //dependency injection
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllAsync()

        {
            return _context.Stocks.ToListAsync();

        }
    }
}
