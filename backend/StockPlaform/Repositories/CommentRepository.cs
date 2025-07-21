using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Interfaces;
using StockPlaform.Models;

namespace StockPlaform.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }




        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();


        }
    }
}
