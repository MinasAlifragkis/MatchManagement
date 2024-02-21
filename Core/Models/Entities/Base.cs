using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities
{
    public class Base
    {
        [Key]
        public long ID { get; set; }
    }
}
