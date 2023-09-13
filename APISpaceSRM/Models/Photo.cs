using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APISpaceSRM.Models
{
    [Serializable]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string FileExtention { get; set; }
        public byte[] Bytes { get; set; }
        public long Size { get; set; }
        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
        public PhotoType Type { get; set; }

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PhotoType
    {
        Before,
        After
    }
}
