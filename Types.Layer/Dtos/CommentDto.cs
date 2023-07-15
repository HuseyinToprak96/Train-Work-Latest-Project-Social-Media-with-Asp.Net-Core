using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Dtos
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
        public UserAppDto User { get; set; }
    }
}
