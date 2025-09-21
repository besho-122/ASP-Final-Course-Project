using Besho.BLL.Services.Classes;
using Besho.BLL.Services.Interfaces;
using Besho.DAL.Data;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Classes;
using Besho.DAL.Repositories.Interfaces;
using Besho.DAL.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar;
using Scalar.AspNetCore;


namespace Besho.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<BLL.Services.Interfaces.IAuthenticationService,BLL.Services.Classes.AuthenticationService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddScoped<ISeedData,SeedData>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();

            }
            var scope = app.Services.CreateScope();
            var objectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objectOfSeedData.DataSeedingAsync();
            await objectOfSeedData.IdentityDataSeedingAsync();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
