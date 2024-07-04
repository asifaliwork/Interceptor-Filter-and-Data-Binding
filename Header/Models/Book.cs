using System.ComponentModel.DataAnnotations;

namespace Header.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }
}
