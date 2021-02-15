using AutoMapper;
using FiveFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveFan.Infrastructure
{
   public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<UserModel, UserVm>();
        }
    }
}
