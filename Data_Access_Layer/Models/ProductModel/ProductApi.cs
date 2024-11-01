﻿using Data_Access_Layer.Models.CategoryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.ProductModel
{
    [Table("Api_Product")]
    public class ProductApi
    {
        [Key]

        public Guid Id { get; set; }

        public string? AdminId { get; set; }

        public int CategoryId { get; set; }

        public bool Available { get; set; }


        [Required(ErrorMessage = "Product Name Is Required")]
        public string? ProductName { get; set;}

        public int ProductPrice { get; set; }

        public bool IsOffer { get; set; }

        public int OfferPrice { get; set; }

        public string? ProductDescription { get; set; }

        public string? Image {  get; set; }

    }
}
