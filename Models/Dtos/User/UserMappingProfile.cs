using AutoMapper;
using MediaApp.Models;
using Models.ModelExtensions;
using Models.Models;
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
                .ForMember(d => d.Password, s => s.MapFrom(x => x.PasswordHash));
            CreateMap<AppUser, LoginDto>()
                .ForMember(d => d.Password, s => s.MapFrom(x => x.PasswordHash));
         

            CreateMap<AppUser, MemberDto>()
                .ForMember(d=> d.PhotoUrl, o=> o.MapFrom(s=> s.Photos.FirstOrDefault(x=> x.IsMain).Url))
                .ForMember(d=> d.Age, o=> o.MapFrom(s=> s.DateOfBirth.CalculateAge()));
            //our calculate age extension method, help us project to the memberdto 
                
            CreateMap<Photo, PhotoDto>();
        }
    }
}
