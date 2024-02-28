using DTO_Layer.DTOsModels.CategoryModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Data_Access_Layer.Models.CategoryModel;

namespace Business_Access_Layer.Interface.Category
{
    public interface CategoryInterface
    {
        Task<string> CategoryCreate(CategoryDTO category);

        public List<CategoryApi> GetCategories();

    }
}