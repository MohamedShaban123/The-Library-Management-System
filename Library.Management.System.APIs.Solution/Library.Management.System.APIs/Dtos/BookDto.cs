using System.ComponentModel.DataAnnotations;

namespace Library.Management.System.APIs.Dtos
{
    public class BookDto
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int PublishedYear { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}
