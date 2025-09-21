using Besho.DAL.Data;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Classes
{
    public class BrandRepository : GenericRepository<Brand>,IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context):base(context) 
        {
           
        }
    }
}
