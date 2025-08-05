using Microsoft.EntityFrameworkCore;
using StockPlaform.Data;
using StockPlaform.Helpers;
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

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;


        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;

        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject)
        {
            //comment is a separate table and AppUSer so we need include 
            var comments =  _context.Comments.Include(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(commentQueryObject.Symbol))
            {

                // only comments related to this stock
                comments = comments.Where(s => s.Stock.Symbol == commentQueryObject.Symbol);

            }
            if (commentQueryObject.IsDecsending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }



            return await comments.ToListAsync();








        }

        public async Task<Comment?> GetByIdAsync(int id)


        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);




        }
        public async Task<Comment> UpdateAsync(Comment comment)
        {
            await _context.SaveChangesAsync();
            return comment;


        }
    }
}
