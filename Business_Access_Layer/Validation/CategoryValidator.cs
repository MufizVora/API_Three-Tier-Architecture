using Data_Access_Layer.Data;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Access_Layer.Validation
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        private readonly Context _context;
        public CategoryValidator(Context context)
        {
            _context = context;


            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Admin ID is required");


            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Must(BeUniqueName).WithMessage("Category name already exists")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters");


            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required")
                .Must(BeValidBase64Image).WithMessage("Image must be a valid JPG or PNG image.");
        }
        private bool BeUniqueName(string name)
        {
            // Check if a category with the given name already exists in the database
            return !_context.Categories.Any(c => c.Name == name);
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
