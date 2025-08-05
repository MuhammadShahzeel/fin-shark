using StockPlaform.Helpers;
using StockPlaform.Models;

namespace StockPlaform.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);

        Task<Comment> UpdateAsync(Comment comment);
        Task<Comment?> DeleteAsync(int id);

    }
}
