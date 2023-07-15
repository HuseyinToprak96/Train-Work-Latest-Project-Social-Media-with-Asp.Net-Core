using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Layer.Contracts.Interfaces
{
    public interface ISharedLikeOrchestration:IGenericOrchestration<SharedLikeContract>
    {
        IEnumerable<string> GetLikedUsers(int sharedId);
    }
}
