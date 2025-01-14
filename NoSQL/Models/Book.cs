using System.ComponentModel.DataAnnotations;

namespace NoSQL.Models
{
    public class Book
    {
        //aaa
        public int id { get; set; }
        public string isbn { get; set; }
        public string opis { get; set; }
        public string tytul { get; set; }
        public string? autor { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataWydanie { get; set; }
        public string? gatunek { get; set; }
        public decimal cena { get; set; }
    }
}
