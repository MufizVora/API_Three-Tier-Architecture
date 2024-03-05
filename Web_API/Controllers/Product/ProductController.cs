using Business_Access_Layer.Interface.Category;
using Business_Access_Layer.Interface.Product;
using Business_Access_Layer.Service.Product;
using Data_Access_Layer.Migrations;
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
        [HttpGet]
        public IActionResult ListProduct()
        {
            var products = _productInterface.GetProducts();
            if (products == null || !products.Any())
            {
                return BadRequest("No products found.");
            }
            return Ok(products);
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
        [HttpGet]
        public IActionResult DetailProduct(Guid id)
        {
            var products = _productInterface.GetProductData(id);
            if (products == null)
            {
                return BadRequest("Product not found.");
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDTO product)
        {
            var response = "";

            try
            {
                response = await _productInterface.ProductEdit(product);

                if (response == "Success")
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
        public IActionResult DeleteProduct(Guid id, string adminId)
        {
            var response = "";

            try
            {
                response = _productInterface.ProductDelete(id, adminId);

                if (response == "Success")
                {
                    return Json(new { message = "Successfully Deleted Product." });
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
