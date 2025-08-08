using Helpers.Helpers;
using MediaApp.Models;
using Models.Dtos.User;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test
{
    public static class UserDataGetter
    {

        public static UserParams GetUserParams()
        {
            UserParams userParams = new UserParams
            {
                PageNumber = 1,
                PageSize = 10,
                Gender = "male",
                CurrentUsername = "lisa",
                MinAge = 18,
                MaxAge = 60
            };

            return userParams;
        }

        public static AppUser GetAppUser()
        {
            var user = new AppUser
            {
                UserName = "Lisanaoooo",
                Gender = "female",
                DateOfBirth = new DateTime(1956, 7, 22),
                KnownAs = "Lisanaoooo",
                CreatedAt = new DateTime(2020, 6, 24),
                LastActive = new DateTime(2020, 6, 21),
                Introduction = "Sunt esse aliqua ullamco in incididunt consequat commodo. Nisi ad esse elit ipsum commodo fugiat est ad. Incididunt nostrud incididunt nostrud sit excepteur occaecat.\r\n",
                LookingFor = "Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                Interests = "Sit sit incididunt proident velit.",
                City = "Greenbush",
                Country = "Martinique",
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        Url = "https://randomuser.me/api/portraits/women/54.jpg",
                        IsMain = true
                    }
                }
            };

            return user;
        }

        public static RegisterDto GetRegisterDto()
        {
            var user = new RegisterDto
            {
                Username = "Lisanaoooo",
                Gender = "female",
                Password = "1",
                DateOfBirth = new DateTime(1956, 7, 22),
                KnownAs = "Lisanaoooo",
                City = "Greenbush",
                Country = "Martinique",
             
            };

            return user;
        }
    }
}
