using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Mapping
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<UserAppDto, AppUserContract>().ReverseMap();
            CreateMap<CreateUserDto, AppUserContract>().ReverseMap();
            CreateMap<SharedDto, SharedContract>().ReverseMap();
            CreateMap<UpdateUserDto,AppUserContract>().ReverseMap();
            CreateMap<CommentDto,CommentContract>().ReverseMap();
            CreateMap<SharedLikeDto, SharedLikeContract>().ReverseMap();
            CreateMap<FollowDto,FollowContract>().ReverseMap();
        }
    }
}
