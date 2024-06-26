﻿using Core.Models.DTO;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseDTO
    {
        Task<ResponseDTO<T>> AddAsync(T entityDTO);
        Task<PagedResponseDTO<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<ResponseDTO<T>> GetByIdAsync(long Id);
        Task<ResponseDTO<T>> UpdateAsync(long Id, T entityDTO);
        Task<ResponseDTO<T>> DeleteByIdAsync(long Id);
    }
}