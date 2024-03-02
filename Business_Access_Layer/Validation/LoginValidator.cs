using Data_Access_Layer.Data;
using DTO_Layer.DTOsModels.UserModelDTO;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Validation
{
    public class LoginValidator : AbstractValidator<UserLogDTO>
    {
        private readonly Context _context;
        public LoginValidator(Context context)
        {
            _context = context;


            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Enter Your Email Address")
               .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Enter Your Password");
        }
    }
}
