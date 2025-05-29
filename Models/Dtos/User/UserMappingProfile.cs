using AutoMapper;
using MediaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.User
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();

            CreateMap<AppUser, RegisterDto>()
                .ForMember(d => d.Username, x => x.MapFrom(s => s.UserName))
                .ForMember(d => d.Password, x=> x.MapFrom(s=> s.PasswordHash))
                .ReverseMap();

            CreateMap<AppUser, LoginDto>()
                .ForMember(d=> d.Username, x=> x.MapFrom(s=>s.UserName))
                .ForMember(d => d.Password, x => x.MapFrom(s => s.PasswordHash))
                .ReverseMap();
        }
    }
}
