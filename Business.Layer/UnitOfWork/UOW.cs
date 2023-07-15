using Business.Layer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Interfaces;

namespace Business.Layer.UnitOfWork
{
    public class UOW : IUnitOfWork
    {
        private readonly AppIdentityDbContext _context;

        public UOW(AppIdentityDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
