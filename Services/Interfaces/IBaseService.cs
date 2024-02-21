using Core.Models.DTO;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseDTO
    {
        Task<ResponseDTO<T>> AddAsync(T entityDTO);
        Task<ResponseDTO<T>> GetAllAsync();
        Task<ResponseDTO<T>> GetByIdAsync(long Id);
        Task<ResponseDTO<T>> UpdateAsync(long Id, T entityDTO);
        Task<ResponseDTO<T>> DeleteByIdAsync(long Id);
    }
}