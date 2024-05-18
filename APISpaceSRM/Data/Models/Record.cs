using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APISpaceSRM.Data.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Wait,
        Work,
        End,
        Abolition
    }
    //public enum BodyType
    //{
    //    Hatchback,
    //    Sedan,
    //    Universal,
    //    Minivan5,
    //    MiniCrossover5,
    //    MiniCrossover7,
    //    Crossover7,
    //    Jeep7,
    //    Bus7
    //}
    //public enum BodySize
    //{
    //    S,M,L,XL,XXL,
    //}
    [Serializable]
    public class Record : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        //Клієнт
        public int ClientId { get; set; }
        public Client Client { get; set; }
        //Машина
        public string Brand { get; set; } = "";
        public string NumberOfCar { get; set; } = "";
        public string GasCount { get; set; } = "";
        //Яку роботу потрібно виконати
        [Required]
        public ICollection<Work> Works { get; set; } = new List<Work>();
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Status Status { get; set; }
        public string BodyType { get; set; }
        public string BodySize { get; set; }
        public bool SendMessage { get; set; } = false;
        public int Discount { get; set; }
        public float Sum { get; set; }
    }
}
