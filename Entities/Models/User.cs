using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}