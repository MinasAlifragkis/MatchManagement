using AutoMapper;
using Core.Interfaces.Data;
using Core.Models.DTO;
using Core.Models.Entities;
using Core.Models.Enums;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BaseService<TEntity, TEntityDTO> : IBaseService<TEntityDTO> where TEntity : Base where TEntityDTO : BaseDTO
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _repository;
        private readonly ILogger<BaseService<TEntity, TEntityDTO>> _logger;

        public BaseService(IMapper mapper, IBaseRepository<TEntity> repository, ILogger<BaseService<TEntity, TEntityDTO>> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> AddAsync(TEntityDTO entityDTO)
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entityToAdd = _mapper.Map<TEntity>(entityDTO);
                var entityAdded = await _repository.CreateAsync(entityToAdd);
                var entityResult = _mapper.Map<TEntityDTO>(entityAdded);
                response.Entities.Add(entityResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> GetAllAsync()
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entities = await _repository.GetAllAsync();
                if (entities != null)
                {
                    var entityDTOs = _mapper.Map<List<TEntityDTO>>(entities);
                    response.Entities.AddRange(entityDTOs);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> GetByIdAsync(long Id)
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entity = await _repository.GetByIdAsync(Id);
                if (entity == null)
                {
                    response.Error = new ErrorDTO
                    {
                        ErrorCode = ErrorCode.EntityNotfound
                    };
                }
                else
                {
                    var entityResult = _mapper.Map<TEntityDTO>(entity);
                    response.Entities.Add(entityResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> UpdateAsync(long Id, TEntityDTO entityDTO)
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entity = await _repository.GetByIdAsync(Id);
                if (entity == null)
                {
                    response.Error = new ErrorDTO
                    {
                        ErrorCode = ErrorCode.EntityNotfound
                    };
                }
                else
                {
                    entity = _mapper.Map<TEntity>(entityDTO);
                    var entityUpdated = await _repository.UpdateAsync(entity);
                    var entityResponse = _mapper.Map<TEntityDTO>(entityUpdated);
                    response.Entities.Add(entityResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> DeleteByIdAsync(long Id)
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entity = await _repository.GetByIdAsync(Id);
                if (entity == null)
                {
                    response.Error = new ErrorDTO
                    {
                        ErrorCode = ErrorCode.EntityNotfound
                    };
                }
                else
                {
                    await _repository.DeleteByIdAsync(Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }
    }
}
