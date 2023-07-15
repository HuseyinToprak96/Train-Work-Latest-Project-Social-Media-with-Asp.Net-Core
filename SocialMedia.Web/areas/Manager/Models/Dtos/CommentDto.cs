using System;

namespace SocialMedia.Web.Areas.Manager.Models.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? TopCommentId { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public int SharedId { get; set; }
    }
}
