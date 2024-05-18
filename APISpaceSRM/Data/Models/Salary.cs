using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Data.Models
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}
