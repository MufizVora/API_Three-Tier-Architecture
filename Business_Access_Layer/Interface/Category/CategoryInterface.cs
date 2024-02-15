using DTO_Layer.DTOsModels.CategoryModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business_Access_Layer.Interface.Category
{
    public interface CategoryInterface
    {
        Task<string> CreateCategory(CategoryDTO category, IFormFile image);
    }
}
