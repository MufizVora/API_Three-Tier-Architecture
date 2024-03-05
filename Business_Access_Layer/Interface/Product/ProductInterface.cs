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

    }
}
