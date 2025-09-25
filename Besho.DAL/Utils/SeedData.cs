using Besho.DAL.Data;
using Besho.DAL.Data.Migrations;
using Besho.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Utils
{

    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser>userManager
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category { Name = "Clothes" },
                    new Category { Name = "Mobiles" }

                    );

            }

            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Brand { Name = "Samsung",MainImage= "b.png" },
                    new Brand { Name = "Apple", MainImage = "b.png" },
                    new Brand { Name = "Nike", MainImage = "b.png" }
                    );

            }

            await _context.SaveChangesAsync();


        }

       public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "m123456789mmo@gmail.com",
                    FullName = "MohammaBishawi",
                    PhoneNumber = "0569620188",
                    UserName = "Besho122",
                    EmailConfirmed = true

                };
                var user2 = new ApplicationUser()
                {
                    Email = "mohabish224@gmail.com",
                    FullName = "MohammadNihad",
                    PhoneNumber = "0569620198",
                    UserName = "Besho",
                    EmailConfirmed = true
                };
                var user3 = new ApplicationUser()
                {
                    Email = "Hamood@gmail.com",
                    FullName = "Hamoodhas",
                    PhoneNumber = "0569620189",
                    UserName = "Hamood",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user1,"Pass@12122");
                await _userManager.CreateAsync(user2,"Pass@12122");
                await _userManager.CreateAsync(user3,"Pass@12122");
                await _userManager.AddToRoleAsync(user1,"Admin");
                await _userManager.AddToRoleAsync(user2,"SuperAdmin");
                await _userManager.AddToRoleAsync(user3,"Customer");



            }
            await _context.SaveChangesAsync();
        }

    }
}