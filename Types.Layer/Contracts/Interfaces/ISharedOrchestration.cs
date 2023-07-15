using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface ISharedOrchestration:IGenericOrchestration<SharedContract>
    {
        CustomResponseDto<IEnumerable<SharedDto>> GetSharedsNotShow();
        Task<CustomResponseDto<NoDataDto>> SharedsShowDto();
        Task<CustomResponseDto<IEnumerable<SharedDto>>> ListDetail(string UserId);
    }
}
