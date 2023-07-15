using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; }//Client hangi API larıma erişebilir?
    }
}
