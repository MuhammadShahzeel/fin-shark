using StockPlaform.Dtos.Stock;
using StockPlaform.Helpers;
using StockPlaform.Models;

namespace StockPlaform.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);//if find didnt find anything it will return null that's why we use nullable type
        Task<Stock?> GetBySymbolAsync(string symbol);

        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);

        Task<Stock?> DeleteAsync(int id);


        //check stock exists
        Task<bool> ExistsAsync(int id);

    }

}
