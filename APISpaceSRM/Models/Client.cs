using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
