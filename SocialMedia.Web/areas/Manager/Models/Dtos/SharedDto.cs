using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Web.Areas.Manager.Models.Dtos
{
    public class SharedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string CreatedUserId { get; set; }
        public string UserId { get; set; }
        public EFileType Type { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentDto> Comments { get; set; }
        public bool IsLike { get; set; }
        public List<string> LikeUsers { get; set; }
    }
}
