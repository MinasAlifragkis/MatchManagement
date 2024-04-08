using System;

namespace Core.Models.DTO
{
    public class PagedResponseDTO<T>: ResponseDTO<T> where T : BaseDTO
    {
        public PagingInfo PagingInfo { get; set; }
    }
}
