using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;

namespace Orchestration.Layer.Orchestrations
{
    public class GenericOrchestration<T> : IGenericOrchestration<T> where T : class
    {
        protected readonly IGenericRepository<T> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork;
        public GenericOrchestration(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<T>> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<T>.Success(200, entity);
        }

        public async Task<CustomResponseDto<IEnumerable<T>>> AddRangeAsync(IEnumerable<T> values)
        {
            await _genericRepository.AddRangeAsync(values);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<IEnumerable<T>>.Success(200, values);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            if(await _genericRepository.AnyAsync(expression))
            return CustomResponseDto<bool>.Success(200, await _genericRepository.AnyAsync(expression));
            return CustomResponseDto<bool>.Fail(404,"Not Found");
        }

        public async Task<CustomResponseDto<NoDataDto>> Delete(int id)
        {
            _genericRepository.Delete(await _genericRepository.GetAsync(id));
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoDataDto>.Success(200);
        }

        public async Task<CustomResponseDto<IEnumerable<T>>> GetAllAsync()
        {
            return CustomResponseDto<IEnumerable<T>>.Success(200,await _genericRepository.GetAllAsync());
        }

        public async Task<CustomResponseDto<T>> GetAsync(int id)
        {
            return CustomResponseDto<T>.Success(200,await _genericRepository.GetAsync(id));
        }

        public async Task<CustomResponseDto<NoDataDto>> Update(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoDataDto>.Success(200);
        }

        public CustomResponseDto<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return CustomResponseDto<IQueryable<T>>.Success(200,_genericRepository.Where(expression));
        }
    }
}
