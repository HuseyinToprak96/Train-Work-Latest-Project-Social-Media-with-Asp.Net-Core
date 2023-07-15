using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;

namespace Orchestration.Layer.Orchestrations
{
    public class FollowOrchestration : GenericOrchestration<FollowContract>, IFollowOrchestration
    {
        public FollowOrchestration(IGenericRepository<FollowContract> genericRepository, IUnitOfWork unitOfWork) : base(genericRepository, unitOfWork)
        {
        }
    }
}
