﻿using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddAsync (Cart cart);
        Task<List<Cart>> GetUserCartAsync(string UserId);
         
        Task<bool> ClearCartAsync(string UserId);

    }
}
