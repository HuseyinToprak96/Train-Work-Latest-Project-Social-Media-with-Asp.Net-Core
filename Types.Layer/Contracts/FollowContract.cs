using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Contracts
{
    public class FollowContract:BaseContract
    {
        public string FollowId { get; set; }
        public AppUserContract Follow { get; set; }
        public string FollowingId { get; set; }
        public AppUserContract Following { get; set; }
    }
}
