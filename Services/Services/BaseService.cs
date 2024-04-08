using AutoMapper;
using Core.Interfaces.Data;
using Core.Models.DTO;
using Core.Models.Entities;
using Core.Models.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BaseService<TEntity, TEntityDTO> : IBaseService<TEntityDTO> where TEntity : Base where TEntityDTO : BaseDTO
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IDistributedCache _redisCache;
        private readonly ILogger<BaseService<TEntity, TEntityDTO>> _logger;
        private readonly string redisPerfix;

        public BaseService(IMapper mapper, IBaseRepository<TEntity> repository, IDistributedCache redisCache, ILogger<BaseService<TEntity, TEntityDTO>> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _logger = logger;
            redisPerfix = typeof(TEntity).Name.ToLower();
        }

        public virtual async Task<ResponseDTO<TEntityDTO>> AddAsync(TEntityDTO entityDTO)
        {
            var response = new ResponseDTO<TEntityDTO>();
            try
            {
                var entityToAdd = _mapper.Map<TEntity>(entityDTO);
                var entityAdded = await _repository.CreateAsync(entityToAdd);
                var entityResult = _mapper.Map<TEntityDTO>(entityAdded);
                await _redisCache.SetStringAsync($"{redisPerfix}:{entityResult.ID}", JsonSerializer.Serialize(entityResult));
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

        public virtual async Task<PagedResponseDTO<TEntityDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var response = new PagedResponseDTO<TEntityDTO>();
            try
            {
                var entities = await _repository.GetAllAsync(pageNumber, pageSize);
                if (entities != null)
                {
                    var entityDTOs = _mapper.Map<List<TEntityDTO>>(entities);
                    response.Entities.AddRange(entityDTOs);
                }
                response.PagingInfo = new PagingInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = _repository.Count()
                };
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
                TEntityDTO entityDTO = null;
                var tEntityDTO = await _redisCache.GetStringAsync($"{redisPerfix}:{Id}");
                if (!String.IsNullOrEmpty(tEntityDTO))
                    entityDTO = JsonSerializer.Deserialize<TEntityDTO>(tEntityDTO);
                else
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
                        entityDTO = _mapper.Map<TEntityDTO>(entity);
                        await _redisCache.SetStringAsync($"{redisPerfix}:{Id}", JsonSerializer.Serialize(entityDTO));
                        
                    }
                }
                if(entityDTO is not null)
                    response.Entities.Add(entityDTO);

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
                    await _redisCache.SetStringAsync($"{redisPerfix}:{Id}", JsonSerializer.Serialize(entityResponse));
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
                    await _redisCache.RemoveAsync($"{redisPerfix}:{Id}");
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
