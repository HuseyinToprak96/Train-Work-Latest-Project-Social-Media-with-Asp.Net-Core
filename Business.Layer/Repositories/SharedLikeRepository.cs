using Business.Layer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;

namespace Business.Layer.Repositories
{
    public class SharedLikeRepository : GenericRepository<SharedLikeContract>,ISharedLikeRepository
    {
        public SharedLikeRepository(AppIdentityDbContext db) : base(db)
        {
        }
    }
}
