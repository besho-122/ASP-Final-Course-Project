using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository repository,IFileService fileService) : base(repository) 
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.Now;
            if (request.MainImage != null) {
               var imagePath= await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }

            if (request.SubImages != null)
            {
                var subImagePaths = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages=subImagePaths.Select(path => new ProductImage { ImageName = path }).ToList();
            }
            return  _repository.Add(entity);
        }

        public async Task<List<ProductResponse>> GetAllProduct(HttpRequest request,bool onlyActive = false)
        {

            var products = _repository.GetAllProductsWithImage();

            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }
            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                MainImage = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList()
                ,Reviews=p.Reviews.Select(r=>new ReviewResponse
                {
                    Id=r.Id,
                    Comment = r.Comment,
                    Rate = r.Rate,
                    FullName=r.User.FullName


                }).ToList(),
            }).ToList();
        

        }

    }
}
