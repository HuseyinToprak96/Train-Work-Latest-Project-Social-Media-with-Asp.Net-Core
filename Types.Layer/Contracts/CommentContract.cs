using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Contracts
{
    public class CommentContract:BaseContract
    {
        public string Comment { get; set; }
        public int TopCommentId { get; set; } = 0;//Eğer 0 dan büyükse bir yorumun cevabıdır.
        [ForeignKey("Shared")]
        public int? SharedId { get; set; }
        public SharedContract Shared { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUserContract User { get; set; }
    }
}
