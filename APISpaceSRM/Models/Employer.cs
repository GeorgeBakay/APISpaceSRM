using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public ICollection<Work> Works { get; set; } = new List<Work>();
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    }
}
