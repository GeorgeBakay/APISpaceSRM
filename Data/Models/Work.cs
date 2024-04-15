using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Data.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Work
    {
        [Key]
        public int Id { get; set; }
        //Працівник, працівники які займаються роботою
        public ICollection<Employer> Employers { get; set; } = new List<Employer>();
        //яку роботу виконують
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int RecordId { get; set; }
        [JsonIgnore]
        public Record Record { get; set; }
        public int Price { get; set; }
        public int TruePrice { get; set; }

        public string DescriptionCost { get; set; } = "";
        public int PriceCost { get; set; } = 0;


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Work other = (Work)obj;
            return Id == other.Id; // Порівняння за унікальним полем, наприклад, Id
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Хеш-код унікального поля, наприклад, Id
        }

    }
}
