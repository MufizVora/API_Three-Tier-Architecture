using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Layer.DTOsModels.CategoryModelDTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }


        public string? AdminId { get; set; }


        [Required(ErrorMessage = "Category Name Is Required")]
        public string? Name { get; set; }


        public string? Image { get; set; }
    }
}
