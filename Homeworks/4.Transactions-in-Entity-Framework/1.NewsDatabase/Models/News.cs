using System.ComponentModel.DataAnnotations;

namespace NewsDatabase.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 5)]
        [ConcurrencyCheck]
        public string Content { get; set; }
    }
}
