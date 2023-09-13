using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        //Назва послуги
        public string Name { get; set; }
        //Тип послуги
        public string Type { get; set; }
        //Процент працівнику
        public int Procent { get; set; }
        //Ціна
        public int Price { get; set; }
        //Скільки робіт зробили цією послугою
        public virtual ICollection<Work> Works { get; set; } = new List<Work>();
        public virtual ICollection<SetService> SetServices { get; set; } = new HashSet<SetService>();


    }
    //public class PolishingService : Service
    //{
    //    public string Name { get; set; } = "Полірування";
    //    public PolishingType PolishingType { get; set; }
    //    public OutsideSize Size { get; set; }
    //    public float Price { get; set; }

    //}
    //public class PolishHeadLightsService : Service
    //{
    //    public string Name { get; set; } = "Полірування фар";
    //    public float Price { get; set; }
    //}
    //public class PolishSalonService : Service
    //{
    //    public string Name { get; set; } = "Полірування диталей в салоні";
    //    public float Price { get; set; }
    //}
    //public class DetailingService : Service
    //{
    //    public string Name { get; set; } = "Детейлінг";
    //    public InsideSize Size { get; set; }
    //    public float Price { get; set; }

    //}
    //public class BodyWashService : Service
    //{
    //    public string Name { get; set; } = "Мийка кузова";
    //    public ServiceType ServiceType { get; set; }
    //    public float Price { get; set; }
    //}
    //public class MotorWashService : Service
    //{
    //    public string Name { get; set; } = "Мийка мотора";
    //    public float Price { get; set; }
    //}
    //public class DistCeramService : Service
    //{
    //    public string Name { get; set; } = "Кераміка дисків";
    //    public float Price { get; set; }
    //}
    //public class CarCeramService : Service
    //{
    //    public string Name { get; set; } = "Кераміка";
    //    public OutsideSize OutsideSize { get; set; }
    //    public int Layers { get; set; }
    //    public float Price { get; set; }

    //}
    //public class WaxService : Service
    //{
    //    public string Name { get; set; } = "Віск";
    //    public float Price { get; set; }
    //}
    //public class AntiRainService : Service
    //{
    //    public string Name { get; set; } = "Анти дощ";
    //    public ServiceType ServiceType { get; set; }
    //    public float Price { get; set; }
    //}

}
