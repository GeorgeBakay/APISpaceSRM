using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Data.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Description { get; set; } = "";
    }
}
