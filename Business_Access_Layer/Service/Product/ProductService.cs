using AutoMapper;
using Business_Access_Layer.Interface.Product;
using Business_Access_Layer.Service.ImageUpload;
using Data_Access_Layer.Data;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.CategoryModel;
using Data_Access_Layer.Models.ProductModel;
using DTO_Layer.DTOsModels.ProductModelDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business_Access_Layer.Service.Product
{
    public class ProductService : ProductInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly MultipleImageUtility _multipleimageUtility;

        public ProductService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, MultipleImageUtility multipleImageUtility)
        {
            _context = context;
            _mapper = mapper;
            _multipleimageUtility = multipleImageUtility;
        }
        //public async Task<string> ProductCreate(ProductDTO product)
        //{
        //    // Check if both category and base64String are provided
        //    if (product == null || string.IsNullOrEmpty(product.Image))
        //    {
        //        return "Failed";
        //    }
        //    var response = "";

        //    try
        //    {
        //        var filepath = _multipleimageUtility.SaveBase64Images(product.Image);
        //        product.Image = filepath;

        //        if (string.IsNullOrEmpty(filepath))
        //        {
        //            return "Failed"; // Image saving failed
        //        }

        //        var ProEntity = _mapper.Map<ProductApi>(product);
        //        ProEntity.Id = Guid.NewGuid(); // Generate a new Guid
        //        ProEntity.Image = filepath; // Assign image file path to entity property

        //        _context.Products.Add(ProEntity);
        //        _context.SaveChanges();

        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error creating product: {ex.Message}");
        //        return "Failed";
        //    }
        //}

        public async Task<string> ProductCreate(ProductDTO product)
        {
            try
            {
                // Check if product and images are provided
                if (product == null || string.IsNullOrEmpty(product.Image))
                {
                    return "Failed";
                }

                // Save base64 images and get file paths
                var imageFileNames = _multipleimageUtility.SaveBase64Images(product.Image);

                // Map DTO to Entity
                var productEntity = _mapper.Map<ProductApi>(product);

                // Generate a new Guid for the product
                productEntity.Id = Guid.NewGuid();

                // Assign concatenated file names to the product entity's Image property
                productEntity.Image = string.Join(",", imageFileNames);

                // Add the product entity to the database context and save changes
                _context.Products.Add(productEntity);
                _context.SaveChanges(); // Use SaveChanges method

                return "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
                return "Failed";
            }
        }
    }
}
