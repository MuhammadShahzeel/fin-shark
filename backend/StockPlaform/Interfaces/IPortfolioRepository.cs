using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockPlaform.Models;
using System;
using System.Collections.Generic;

namespace StockPlaform.Interfaces
{
    public interface IPortfolioRepository
    {
        // Interface for Portfolio Repository

        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);

//        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);
//        iska kaam yeh hi hai ke:

//"Yeh check karo ke kisi specific user ke paas kaun kaun se stocks hain."

//Matlab:

//Har user ne apne portfolio me kuch stocks save kiye honge(through join table Portfolio)

//Yeh method usi join table ko check karke return karega un stocks ki list jo is user ke pass hain
    }
}
