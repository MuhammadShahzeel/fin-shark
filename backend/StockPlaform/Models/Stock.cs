using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlaform.Models
{
    [Table("Stocks")] // Ye table ka naam hai jo database mein banegas
    public class Stock
    {
      
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty; //not null 

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")] // it accepts total  max 18 digits, with 2 decimal (16 before . 2 after .) places eg: 100.00,
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } =string.Empty;
        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>(); // // This list stores objects of the Comment class
        //one to many relationship 
        //a Stock has many comments

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        //Ye line batati hai ke ek Stock multiple Portfolios mein ho sakta hai
        // one to many relationship
        //Ek Stock ke paas multiple Portfolio items ho sakte hain



    }
}
