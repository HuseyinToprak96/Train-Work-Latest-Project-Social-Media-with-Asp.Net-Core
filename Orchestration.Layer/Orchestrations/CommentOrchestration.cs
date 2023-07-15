using Microsoft.AspNetCore.Identity;
using Orchestration.Layer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class CommentOrchestration : GenericOrchestration<CommentContract>, ICommentOrchestration
    {
        private readonly UserManager<AppUserContract> _userManager;
        private readonly ICommentRepository _commentRepository;

        public CommentOrchestration(IGenericRepository<CommentContract> genericRepository, IUnitOfWork unitOfWork, UserManager<AppUserContract> userManager, ICommentRepository commentRepository) : base(genericRepository, unitOfWork)
        {
            _userManager = userManager;
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentDto>> GetSharedComments(int sharedId)
        {
            var comments=await _commentRepository.GetAllAsync();
            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (var item in comments)
            {
                commentDtos.Add(new CommentDto
                {
                    Id = item.Id,
                    Comment = item.Comment,
                    CreatedDate = item.CreatedDate,
                    TopCommentId = item.TopCommentId,
                    Username = _userManager.Users.FirstOrDefault(x => x.Id == item.UserId).UserName
                });
            }
            return commentDtos; 
        }
    }
}
