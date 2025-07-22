using StockPlaform.Dtos.Comment;
using StockPlaform.Dtos.Stock;
using StockPlaform.Models;

namespace StockPlaform.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId

            };
        }
    }
}
