using Data_Access_Layer.Data;
using DTO_Layer.DTOsModels.ProductModelDTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Access_Layer.Validation
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public readonly Context _context;
        public ProductValidator(Context context)
        {
            _context = context;


            RuleFor(x => x.AdminId).NotEmpty().WithMessage("AdminId is required.");


            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");


            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name is required.");


            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Product Price must be greater than zero.");


            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Product Description is required.");


            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.")
            .Must(BeValidBase64).WithMessage("Image must be a valid base64 string.")
            .Must(BeValidBase64Image).WithMessage("Image must be a valid JPG or PNG image.");



            RuleFor(x => x.OfferPrice).GreaterThan(0).When(x => x.IsOffer).WithMessage("Offer Price must be greater than zero if IsOffer is true.");


            // Custom rule for checking Offer Price only if IsOffer is true
            RuleFor(x => x)
                .Must(x => !x.IsOffer || x.OfferPrice > 0)
                .WithMessage("Offer Price must be greater than zero if IsOffer is true.");


            // Custom rule for ProductName length
            RuleFor(x => x.ProductName).MaximumLength(100).WithMessage("Product Name cannot be longer than 100 characters.");


            // Custom rule for ProductDescription length
            RuleFor(x => x.ProductDescription).MaximumLength(500).WithMessage("Product Description cannot be longer than 500 characters.");
        }
        private bool BeValidBase64(string? image)
        {
            if (string.IsNullOrEmpty(image))
                return false;

            // Check if the string is a valid base64 string
            var base64Regex = new Regex(@"^(data:image\/[a-zA-Z]+;base64,)[\w\+\/\=]+$");
            return base64Regex.IsMatch(image);
        }
        private async Task<bool> BeUniqueProductName(string? productName)
        {
            if (string.IsNullOrEmpty(productName))
                return false;

            // Check if the product name already exists in the database
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);

            // If a product with the same name exists, return false (validation failed)
            // and set the custom error message
            if (existingProduct != null)
            {
                // Set the error message using FluentValidation's WithMessage method
                // You can customize this message as needed
                RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product is already exists");
                return false;
            }

            return true;
        }
        private bool BeValidBase64Image(string base64Image)
        {
            if (string.IsNullOrWhiteSpace(base64Image))
                return false;

            // Optional: Remove the MIME type prefix if present
            var base64Data = Regex.Replace(base64Image, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);

            // Check if the base64 string, after removing the MIME type, starts with the headers for JPG or PNG images
            // This regex looks for the start of base64-encoded data that typically represents JPG or PNG images
            // Adjust the pattern to match the specific characteristics of your base64 data if necessary
            return Regex.IsMatch(base64Data, @"^(\/9j\/|iVBORw0KGgo)");
        }
    }
}
