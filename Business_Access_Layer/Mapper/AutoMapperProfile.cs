using AutoMapper;
using Data_Access_Layer.Models.UserModel;
using DTO_Layer.DTOsModels.UserModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserApi, UserRegDTO>().ReverseMap();
        }
    }
}
