using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class SharedLikeOrchestration : GenericOrchestration<SharedLikeContract>, ISharedLikeOrchestration
    {
        private readonly ISharedLikeRepository _sharedLikeRepository;
        private readonly UserManager<AppUserContract> _userManager;
        public SharedLikeOrchestration(IGenericRepository<SharedLikeContract> genericRepository, IUnitOfWork unitOfWork, ISharedLikeRepository sharedLikeRepository, UserManager<AppUserContract> userManager) : base(genericRepository, unitOfWork)
        {
            _sharedLikeRepository = sharedLikeRepository;
            _userManager = userManager;
        }
        public IEnumerable<string> GetLikedUsers(int sharedId)
        {
                var sharedLikes = _sharedLikeRepository.Where(x => x.SharedId == sharedId).ToList();
                foreach (var item in sharedLikes)
                {
                    yield return _userManager.Users.FirstOrDefault(x => x.Id == item.UserId).UserName;
                }
        }
    }
}
