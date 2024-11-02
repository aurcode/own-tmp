using AutoMapper;
using Core.Autentication;
using Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Mappers
{
    public class MapperUser : Profile
    {
        public MapperUser()
        {
            CreateMap<User, CreateOrEditUserDto>();
            CreateMap<CreateOrEditUserDto, User>();
        }
    }
}
