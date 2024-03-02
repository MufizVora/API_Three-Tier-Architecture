using Data_Access_Layer.Data;
using DTO_Layer.DTOsModels.CategoryModelDTO;
using DTO_Layer.DTOsModels.UserModelDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Validation
{
    public class RegisterValidator : AbstractValidator<UserRegDTO>
    {
        public readonly Context _context;
        public RegisterValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Enter Your UserName");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Enter Your Email Address")
                .Must(IsValidEmail).WithMessage("Invalid Email Address")
                .Must((dto, email) => !IsDuplicateUser(dto.Email, email)).WithMessage("Email Is Already Exists");



            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Enter Your Phone Number")
            .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits long")
            .Must((dto, phoneNumber) => !IsDuplicatePhoneNumber(phoneNumber)).WithMessage("Phone Number Is Already Registered");




            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Enter Your Password")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one digit, and be at least 6 characters long");


            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match")
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Enter Your Confirm Password");
        }
        public bool IsDuplicateUser(string name, string email)
        {
            var data = _context.Users.Where(u => u.Name == name || u.Email == email);
            bool isDuplicate = data != null && data.Any();
            return isDuplicate;

            // var data = _context.Users.Where(u => u.Name == name || u.Email == email);
            // return data.Any();
        }
        public bool IsValidEmail(string email)
        {
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
        }
        private bool IsDuplicatePhoneNumber(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }
    }
}
