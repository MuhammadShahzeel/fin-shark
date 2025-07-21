using StockPlaform.Dtos.Stock;
using StockPlaform.Models;

namespace StockPlaform.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);//if finddidnt find anyrhung it will return null that's why we use nullable type

        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);

        Task<Stock?> DeleteAsync(int id);

    }

}
