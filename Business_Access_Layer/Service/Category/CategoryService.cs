using AutoMapper;
using Business_Access_Layer.Interface.Category;
using Bussiness_Access_Layer.Service.ImageUpload;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.CategoryModel;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Service.Category
{
    public class CategoryService : CategoryInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly CategoryImage _image;

        public CategoryService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, CategoryImage image)
        {
            _context = context;
            _mapper = mapper;
            _image = image;
        }
        public async Task<string> CreateCategory(CategoryDTO category, IFormFile image)
        {
            var response = "";

            try
            {
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                var filepath = await _image.UploadCategoryFile(image);
                category.Image = filepath;

                var CatEntity = _mapper.Map<CategoryApi>(category);
                _context.Categories.Add(CatEntity);
                _context.SaveChanges();

                response = "Success";
                return response;
            }
            catch (Exception)
            {
                response = "Failed";
                return response;
            }
        }
    }
}
