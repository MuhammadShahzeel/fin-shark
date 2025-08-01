﻿using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Dtos.Stock;
using StockPlaform.Helpers;
using StockPlaform.Interfaces;
using StockPlaform.Mappers;
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

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;


        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;

        }

        public async Task<bool> ExistsAsync(int id)
        {

            //use AnyAsync to check if a stock with the given id exists
            //we use any async because it is more efficient for existence checks
            //it will return true if it exists, false otherwise not whole object
            return await _context.Stocks.AnyAsync(i => i.Id == id);

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)

        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrEmpty(query.CompanyName)) {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            }

            if (!string.IsNullOrEmpty(query.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending
                        ? stocks.OrderByDescending(s => s.Symbol)
                        : stocks.OrderBy(s => s.Symbol);
                }
                //yeh men khud lgayg hy 
                else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                //ab aapko case-insensitive comparison karna ho, tab StringComparison.OrdinalIgnoreCase use karte hain. eg: apple == Apple
                {
                    stocks = query.IsDescending
                        ? stocks.OrderByDescending(s => s.CompanyName)
                        : stocks.OrderBy(s => s.CompanyName);
                }
            }

            //pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
         

            
        

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i=>i.Id==id);

        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s=>s.Symbol == symbol);
      

        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
    
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
