using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface IGenericOrchestration<T> where T : class
    {
        Task<CustomResponseDto<T>> GetAsync(int id);
        Task<CustomResponseDto<IEnumerable<T>>> GetAllAsync();
        CustomResponseDto<IQueryable<T>> Where(Expression<Func<T, bool>> expression);
        Task<CustomResponseDto<T>> AddAsync(T entity);
        Task<CustomResponseDto<IEnumerable<T>>> AddRangeAsync(IEnumerable<T> values);
        Task<CustomResponseDto<NoDataDto>> Update(T entity);
        Task<CustomResponseDto<NoDataDto>> Delete(int id);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
