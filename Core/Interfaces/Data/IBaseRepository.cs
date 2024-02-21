using Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Data
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long Id);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(long Id);
    }
}
