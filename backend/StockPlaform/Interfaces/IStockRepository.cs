using StockPlaform.Models;

namespace StockPlaform.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        //Task<Stock?> GetStockByIdAsync(int id);
        //Task<Stock?> GetStockBySymbolAsync(string symbol);
        //Task<Stock> CreateStockAsync(CreateStockRequestDto stockDto);
        //Task<Stock> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
        //Task<bool> DeleteStockAsync(int id);
        //Task<IEnumerable<Comment>> GetCommentsByStockIdAsync(int stockId);
        //Task<Comment> AddCommentToStockAsync(int stockId, Comment comment);

    }

}
