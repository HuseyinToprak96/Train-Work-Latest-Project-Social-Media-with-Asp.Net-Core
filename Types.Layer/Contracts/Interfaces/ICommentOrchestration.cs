using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface ICommentOrchestration:IGenericOrchestration<CommentContract>
    {
        Task<IEnumerable<CommentDto>> GetSharedComments(int sharedId);
    }
}
