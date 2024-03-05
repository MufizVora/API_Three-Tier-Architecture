using Business_Access_Layer.Interface.Category;
using Business_Access_Layer.Interface.Product;
using Business_Access_Layer.Service.Product;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using DTO_Layer.DTOsModels.ProductModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers.Product
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductInterface _productInterface;

        public ProductController(ProductInterface productInterface)
        {
            _productInterface = productInterface;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            var response = "";

            try
            {
                response = await _productInterface.ProductCreate(product);
                if (response == "Success")
                {
                    return Json(new { message = "Successfully Added Product." });
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
