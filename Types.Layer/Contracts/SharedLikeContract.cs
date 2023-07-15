using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Contracts
{
    public class SharedLikeContract
    {
        public int Id { get; set; }
        [ForeignKey("Shared")]
        public int? SharedId { get; set; }
        public SharedContract Shared { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUserContract User { get; set; }
    }
}
