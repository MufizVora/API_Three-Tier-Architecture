using Business_Access_Layer.Interface.User;
using Business_Access_Layer.Mapper;
using Business_Access_Layer.Service.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bussiness_Access_Layer.BussinessInjection
{
    public static class BussinessDependency
    {
        public static void BussinessBLL(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddScoped<UserInterface, UserService>();
            builder.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}