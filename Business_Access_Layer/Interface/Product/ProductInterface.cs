using Data_Access_Layer.Models.ProductModel;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using DTO_Layer.DTOsModels.ProductModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Interface.Product
{
    public interface ProductInterface
    {
        Task<string> ProductCreate(ProductDTO product);

        public List<ProductApi> GetProducts();

        public ProductApi GetProductData(Guid id);

        Task<string> ProductEdit(ProductDTO product);

        public string ProductDelete(Guid id, string adminId);
    }
}
