using AutoMapper;
using e_commerce_project.Controllers;
using e_commerce_project.Modles;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace e_commerce_project.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<Users, UsersDto>().ReverseMap();
        }

    }
}
