using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.CategoryModel
{
    [Table("Api_Category")]
    public class CategoryApi
    {
        [Key]

        public Guid Id { get; set; }


        public string? AdminId { get; set; }


        [Required(ErrorMessage ="Category Name Is Required")]
        public string? Name { get; set; }


        public string? Image {  get; set; }
    }
}
