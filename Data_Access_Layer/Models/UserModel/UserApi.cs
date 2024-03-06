using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data_Access_Layer.Models.UserModel
{
    [Table("Api_User")]
    public class UserApi
    {
        [Key]

        public Guid Id { get; set; }


        [Required(ErrorMessage = "Enter Your UserName")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Enter Your Email Address")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Enter Your Password")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Enter Your Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpiration { get; set; }

    }
}
