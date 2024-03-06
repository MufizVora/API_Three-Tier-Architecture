using AutoMapper;
using Business_Access_Layer.Interface.Category;
using Business_Access_Layer.Service.ImageUpload;
using Data_Access_Layer.Data;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.CategoryModel;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business_Access_Layer.Service.Category
{
    public class CategoryService : CategoryInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ImageUtility _image;

        public CategoryService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ImageUtility image)
        {
            _context = context;
            _mapper = mapper;
            _image = image;
        }

        public async Task<string> CategoryCreate(CategoryDTO category)
        {
            // Check if both category and base64String are provided
            if (category == null || string.IsNullOrEmpty(category.Image))
            {
                return "Failed";
            }

            try
            {
                var filepath = _image.SaveBase64Image(category.Image);
                category.Image = filepath;

                if (string.IsNullOrEmpty(filepath))
                {
                    return "Failed"; // Image saving failed
                }

                var CatEntity = _mapper.Map<CategoryApi>(category);
                CatEntity.Id = Guid.NewGuid(); // Generate a new Guid
                CatEntity.Image = filepath; // Assign image file path to entity property

                _context.Categories.Add(CatEntity);
                _context.SaveChanges();

                return "Success";
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
                return "Failed";
            }
        }

        public List<CategoryApi> GetCategories()
        {
            //if you want to get list of categories with object

            //var data = _context.Categories.Select(a => new CategoryApi
            //    {
            //    Id = a.Id,
            //    AdminId = a.AdminId,
            //    Name = a.Name,
            //    Image = a.Image
            //}).ToList();
            //return data;

            //if you want to get list of categories with mapper

            var categories = _context.Categories.ToList();
            // Perform mapping using AutoMapper
            var data = _mapper.Map<List<CategoryApi>>(categories);
            return data;
        }

        public CategoryApi GetCategoryData(Guid id)
        {
            var data = _context.Categories.FirstOrDefault(a => a.Id == id);
            return data;
        }

        //public async Task<string> CategoryEdit(CategoryDTO category)
        //{
        //    // Check if both category and base64String are provided
        //    if (category == null || string.IsNullOrEmpty(category.Image) || category.Id == Guid.Empty)
        //    {
        //        return "Failed";
        //    }

        //    var response = "";

        //    try
        //    {
        //        var existingCategory = _context.Categories.Find(category.Id);

        //        if (existingCategory == null)
        //        {
        //            string newFilePath = null;

        //            if (category.Image != null)
        //            {
        //                // Handle image upload logic (save to server, update database)
        //                newFilePath = _image.SaveBase64Image(category.Image);
        //            }
        //            // Update other properties of the category
        //            existingCategory.Name = category.Name; // Assuming there's a Name property in CategoryApi

        //            // If a new file is uploaded, update the file path; otherwise, keep the existing file
        //            existingCategory.Image = newFilePath ?? existingCategory.Image;


        //            _context.Categories.Update(existingCategory);
        //            _context.SaveChanges();

        //            return "Success";
        //        }
        //        else
        //        {
        //            response = "Category not found";
        //        }
        //        return response;
        //        //// If a new image is provided, save it
        //        //if (!string.IsNullOrEmpty(category.Image))
        //        //{
        //        //    var filepath = _image.SaveBase64Image(category.Image);

        //        //    if (string.IsNullOrEmpty(filepath))
        //        //    {
        //        //        return "Failed"; // Image saving failed
        //        //    }

        //        //    existingCategory.Image = filepath; // Update image file path
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error editing category: {ex.Message}");
        //        return "Failed";
        //    }
        //}

        public async Task<string> CategoryEdit(CategoryDTO category)
        {
            // Check if both category and base64String are provided
            if (category == null || string.IsNullOrEmpty(category.Image) || category.Id == Guid.Empty)
            {
                return "Failed";
            }

            try
            {
                var existingCategory = _context.Categories.Find(category.Id);

                if (existingCategory != null)
                {
                    // If a new image is provided, save it
                    if (!string.IsNullOrEmpty(category.Image))
                    {
                        var newFilePath = _image.SaveBase64Image(category.Image);

                        if (string.IsNullOrEmpty(newFilePath))
                        {
                            return "Failed"; // Image saving failed
                        }

                        existingCategory.Image = newFilePath; // Update image file path
                    }

                    // Update other properties of the category
                    existingCategory.Name = category.Name; // Assuming there's a Name property in CategoryApi
                    existingCategory.AdminId = category.AdminId;

                    _context.Categories.Update(existingCategory);
                    _context.SaveChanges();

                    return "Success";
                }
                else
                {
                    return "Category not found";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing category: {ex.Message}");
                return "Failed";
            }
        }

        public string CategoryDelete(Guid id, string adminId)
        {
            var response = "";

            try
            {
                var category = _context.Categories.Any(a => a.Id == id && a.AdminId == adminId);

                if (category)
                {
                    // Find the category by ID
                    var categoryToDelete = _context.Categories.Find(id);

                    if (categoryToDelete != null)
                    {
                        _context.Categories.Remove(categoryToDelete);
                        _context.SaveChanges();

                        response = "Success";
                    }
                    else
                    {
                        response = "Category not found";
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
