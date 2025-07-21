using StockPlaform.Models;

namespace StockPlaform.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

    }
}
