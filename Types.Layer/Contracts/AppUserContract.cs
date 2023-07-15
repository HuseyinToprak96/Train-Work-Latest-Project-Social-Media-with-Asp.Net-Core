using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Contracts
{
    public class AppUserContract:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsShow { get; set; } = false;
        public IEnumerable<SharedContract> Shareds { get; set; }
        public IEnumerable<SharedLikeContract> SharedLikes { get; set; }
        public IEnumerable<CommentContract> Comments { get; set; }
        public IEnumerable<FollowContract> Follows { get; set; }
        public IEnumerable<FollowContract> Followings { get; set; }
    }
}
