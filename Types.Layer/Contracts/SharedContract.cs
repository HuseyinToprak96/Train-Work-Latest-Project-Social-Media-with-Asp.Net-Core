using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Enums;

namespace Types.Layer.Contracts
{
    public class SharedContract:BaseContract
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string CreatedUserId { get; set; }//Bunu koyma sebebimiz başka kullanıcıların gönderisini paylaşabiliyoruz. Bu gönderinin asıl sahibi kim?
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUserContract User { get; set; }
        public EFileType Type { get; set; }
        public IEnumerable<CommentContract> Comments { get; set; }
        public IEnumerable<SharedContract> Likes { get; set; }
    }
}
