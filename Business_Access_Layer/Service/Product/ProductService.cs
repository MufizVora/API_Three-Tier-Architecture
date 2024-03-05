using AutoMapper;
using Business_Access_Layer.Interface.Product;
using Business_Access_Layer.Service.ImageUpload;
using Data_Access_Layer.Data;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.CategoryModel;
using Data_Access_Layer.Models.ProductModel;
using DTO_Layer.DTOsModels.ProductModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public List<ProductApi> GetProducts()
        {
            var products = _context.Products.ToList();
            var data = _mapper.Map<List<ProductApi>>(products);
            return data;
        }

        public async Task<string> ProductCreate(ProductDTO product)
        {
            try
            {
                // Check if product and images are provided
                if (product == null || string.IsNullOrEmpty(product.Image))
                {
                    return "Failed";
                }

                // If IsOffer is false, OfferPrice can be 0
                if (!product.IsOffer && product.OfferPrice != 0)
                {
                    return "Failed: Offer Price must be 0 when IsOffer is false.";
                }

                // If IsOffer is true, OfferPrice cannot be 0
                if (product.IsOffer && product.OfferPrice == 0)
                {
                    return "Failed: Offer Price is required when IsOffer is true.";
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
        public ProductApi GetProductData(Guid id)
        {
            var data = _context.Products.FirstOrDefault(p => p.Id == id);
            return data;
        }
        public async Task<string> ProductEdit(ProductDTO product)
        {
            try
            {
                // Check if product is provided
                if (product == null)
                {
                    return "Failed: Product is null.";
                }

                // Find the existing product entity in the database
                var existingProduct = await _context.Products.FindAsync(product.Id);

                // Check if the product exists
                if (existingProduct == null)
                {
                    return "Failed: Product not found.";
                }

                // Check if images are provided
                if (product.Image == null || !product.Image.Any())
                {
                    return "Failed: Images are required.";
                }

                // Save base64 images and get file paths
                var imageFileNames = _multipleimageUtility.SaveBase64Images(product.Image);

                // Update existing product entity with new values
                existingProduct.AdminId = product.AdminId;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Available = product.Available;
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductPrice = product.ProductPrice;
                existingProduct.IsOffer = product.IsOffer;
                existingProduct.OfferPrice = product.OfferPrice;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.Image = string.Join(",", imageFileNames);

                _context.Products.Update(existingProduct);
                // Save changes to the database
                _context.SaveChangesAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing product: {ex.Message}");
                return "Failed";
            }
        }

        public string ProductDelete(Guid id, string adminId)
        {
            var response = "";

            try
            {
                var product = _context.Products.Any(a => a.Id == id && a.AdminId == adminId);

                if (product)
                {
                    // Find the category by ID
                    var productToDelete = _context.Products.Find(id);

                    if (productToDelete != null)
                    {
                        _context.Products.Remove(productToDelete);
                        _context.SaveChanges();

                        response = "Success";
                    }
                    else
                    {
                        response = "Product not found";
                    }
                }
                else
                {
                    response = "Unauthorized admin";
                }
                return response;
            }
            catch
            {
                return "Failed";
            }

        }
    }
}
