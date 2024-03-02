using Business_Access_Layer.Interface.Category;
using Business_Access_Layer.Interface.User;
using Business_Access_Layer.Mapper;
using Business_Access_Layer.Service.Category;
using Business_Access_Layer.Service.ImageUpload;
using Business_Access_Layer.Service.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Business_Access_Layer.Validation;

namespace Bussiness_Access_Layer.BussinessInjection
{
    public static class BussinessDependency
    {
        public static void BussinessBLL(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddScoped<UserInterface, UserService>();
            builder.AddAutoMapper(typeof(AutoMapperProfile));
            builder.AddScoped<CategoryInterface, CategoryService>();
            builder.AddScoped<ImageUtility>();
            builder.AddHttpContextAccessor();
            builder.AddFluentValidationAutoValidation();
            builder.AddValidatorsFromAssemblyContaining<RegisterValidator>();
            builder.AddValidatorsFromAssemblyContaining<LoginValidator>();
            builder.AddValidatorsFromAssemblyContaining<CategoryValidator>();
        }
    }
}