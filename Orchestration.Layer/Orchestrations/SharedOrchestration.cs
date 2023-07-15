using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Orchestration.Layer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class SharedOrchestration : GenericOrchestration<SharedContract>, ISharedOrchestration
    {
        private readonly ISharedRepository _sharedRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISharedLikeRepository _likeRepository;
        private readonly UserManager<AppUserContract> _userManager;
        public SharedOrchestration(IGenericRepository<SharedContract> genericRepository, IUnitOfWork unitOfWork, ISharedRepository sharedRepository, UserManager<AppUserContract> userManager, ICommentRepository commentRepository, ISharedLikeRepository likeRepository) : base(genericRepository, unitOfWork)
        {
            _sharedRepository = sharedRepository;
            _userManager = userManager;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
        }

        public CustomResponseDto<IEnumerable<SharedDto>> GetSharedsNotShow()
        {
            var shareds = _genericRepository.Where(x => !x.IsShow).ToList();
            if (shareds==null)
            {
                return CustomResponseDto<IEnumerable<SharedDto>>.Fail(404, "Yeni Bir Gönderi Eklenmedi!");
            }
            return CustomResponseDto<IEnumerable<SharedDto>>.Success(200, ObjectMapper.Mapper.Map<IEnumerable<SharedDto>>(shareds));
        }

        public async Task<CustomResponseDto<NoDataDto>> SharedsShowDto()
        {
            var shareds = _genericRepository.Where(x => !x.IsShow).ToList();
            foreach (var shared in shareds)
            {
                shared.IsShow = !shared.IsShow;
                _sharedRepository.Update(shared);
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoDataDto>.Success(200);
        }
        public async Task<CustomResponseDto<IEnumerable<SharedDto>>> ListDetail(string UserId)
        {
            var Shareds = await _sharedRepository.GetAllAsync();
            var Users = _userManager.Users.ToList();
            var Comments=await _commentRepository.GetAllAsync();
            var result = (from s in Shareds
                          join u in Users
                          on s.UserId equals u.Id
                          where !s.IsDeleted && s.IsActive
                          select new SharedDto
                          {
                               Id = s.Id,
                                Username = u.UserName,
                                 Title=s.Title,
                                  Description=s.Description,
                                   Path=s.Path,
                                    Type = s.Type,
                                     CreatedUserId = s.CreatedUserId,
                                      CreatedDate = s.CreatedDate,
                                       UserId = s.UserId,
                              Comments =  (from c in Comments
                                                                 where c.SharedId == s.Id && !c.IsDeleted && c.IsActive
                                                                 select new CommentDto
                                                                 {
                                                                     Id = c.Id,
                                                                     Comment = c.Comment,
                                                                     CreatedDate = c.CreatedDate,
                                                                     TopCommentId = c.TopCommentId,
                                                                     Username = (from cu in Users
                                                                                 where cu.Id == c.UserId
                                                                                 select cu.UserName).FirstOrDefault(),
                                                                     UserId=c.UserId
                                                                 }).ToList(),
                              IsLike=_likeRepository.AnyAsync(x=>x.UserId==UserId && x.SharedId==s.Id).Result,

                          }).OrderByDescending(x=>x.CreatedDate);
            return CustomResponseDto<IEnumerable<SharedDto>>.Success(200,result);
        }
        private async Task<bool> Control(int sharedId)
        {
            return await _commentRepository.AnyAsync(x=>x.SharedId==sharedId);
        }
    }
}
