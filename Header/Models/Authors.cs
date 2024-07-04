using System.ComponentModel.DataAnnotations;

namespace Header.Models
{
    public class Authors
    {
        [Key]
        public int Author_Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
