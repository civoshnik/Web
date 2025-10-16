using System.ComponentModel.DataAnnotations;

namespace Web
{
    public class Usluga
    {
        public enum UslugaEnum
        {
            Published,
            Unpublished
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}