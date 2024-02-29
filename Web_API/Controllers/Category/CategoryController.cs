using Business_Access_Layer.Interface.Category;
using Business_Access_Layer.Service.Category;
using Business_Access_Layer.Service.ImageUpload;
using Data_Access_Layer.Migrations;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Web_API.Controllers.Category
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryInterface _categoryInterface;

        public CategoryController(CategoryInterface categoryInterface)
        {
            _categoryInterface = categoryInterface;
        }

        [HttpGet]
        public IActionResult ListCategory()
        {
            var categories = _categoryInterface.GetCategories();
            return Ok(categories);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO category)
        {
            var response = "";

            try
            {
                response =await _categoryInterface.CategoryCreate(category);
                if (response == "Success")
                {
                    return Json(new { message = "Successfully Added Category." });
                }
                else
                {
                    return BadRequest(new { message = "There is something wrong! try again later." });
                }
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "An Error occurred while processing your request. Please try again later." });
            }
        }
        [HttpGet]
        public IActionResult DetailCategory(Guid id)
        {
            var Categories = _categoryInterface.GetCategoryData(id);
            return Ok(Categories);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryDTO category)
        {
            var response = "";

            try
            {
                response = await _categoryInterface.CategoryEdit(category);

                if(response == "Success")
                {
                    return Json(new { message = "Successfully Edited Category." });
                }
                else
                {
                    return BadRequest(new { message = "There is something wrong! try again later." });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An Error occurred while processing your request. Please try again later." });
            }
        }
        [HttpPost]
        public IActionResult DeleteCategory(Guid id, string adminId)
        {
            var response = "";

            try
            {
                response = _categoryInterface.CategoryDelete(id, adminId);

                if(response == "Success")
                {
                    return Json(new { message = "Successfully Deleted Category." });
                }
                else
                {
                    return BadRequest(new { message = "There is something wrong! try again later." });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An Error occurred while processing your request. Please try again later." });
            }
        }
    }
}
