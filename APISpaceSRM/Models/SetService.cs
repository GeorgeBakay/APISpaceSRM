using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Models
{
    public class SetService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Service> Services{ get; set; } = new List<Service>();
        public virtual ICollection<Record> Records { get; set; } = new List<Record>();
        [Required]
        public int Discount { get; set; }

    }
}
