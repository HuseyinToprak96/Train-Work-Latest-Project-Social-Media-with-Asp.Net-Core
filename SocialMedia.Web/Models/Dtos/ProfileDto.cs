using SocialMedia.Web.Areas.Manager.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Web.Models.Dtos
{
    public class ProfileDto
    {
        public UpdateUserDto User { get; set; }
        public List<SharedDto> SharedDtos { get; set; }
        public List<UserAppDto> Followers { get; set; }
        public List<UserAppDto> Followings { get; set; }
    }
}
