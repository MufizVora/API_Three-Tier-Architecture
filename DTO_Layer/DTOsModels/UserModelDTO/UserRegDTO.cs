using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Layer.DTOsModels.UserModelDTO
{
    public class UserRegDTO
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Enter Your UserName")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Enter Your Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter Your Phone Number")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Enter Your Password")]
        public string? Password { get; set; }


        [Compare("Password")]
        [Required(ErrorMessage = "Enter Your Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
