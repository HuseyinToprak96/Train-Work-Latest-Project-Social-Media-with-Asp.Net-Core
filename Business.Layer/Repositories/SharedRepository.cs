using Business.Layer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Business.Layer.Repositories
{
    public class SharedRepository : GenericRepository<SharedContract>,ISharedRepository
    {
        public SharedRepository(AppIdentityDbContext db) : base(db)
        {
        }
        
    }
}
