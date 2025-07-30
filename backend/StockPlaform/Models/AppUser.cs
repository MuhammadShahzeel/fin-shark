using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockPlaform.Models
{
    public class AppUser:IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
//        Ye line add karne ka main purpose hai:
//AppUser ko Portfolio se navigation property provide karna — taake jab bhi kisi user ko fetch karo, uske saath related portfolio items(stocks) bhi mil sakein.




    }
}
