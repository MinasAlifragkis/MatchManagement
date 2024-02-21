using System.Collections.Generic;

namespace Core.Models.DTO
{
    public class ResponseDTO<T> where T : BaseDTO
    {
        public bool Succeeded { get; set; } = true;
        public List<T> Entities { get; set; } = new List<T>();

        public ErrorDTO Error { get; set; }
    }
}
