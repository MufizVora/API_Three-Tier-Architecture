using Business_Access_Layer.Interface.Category;
using Microsoft.AspNetCore.Mvc;

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
    }
}
