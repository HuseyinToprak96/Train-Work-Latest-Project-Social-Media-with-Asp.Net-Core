using Business.Layer.DataContext;
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
    public class CommentRepository : GenericRepository<CommentContract>,ICommentRepository
    {
        public CommentRepository(AppIdentityDbContext db) : base(db)
        {
        }
        //public async Task<CustomResponseDto<IEnumerable<SharedDto>>> ListDetail()
        //{
        //    var shared=
        //}
    }
}
