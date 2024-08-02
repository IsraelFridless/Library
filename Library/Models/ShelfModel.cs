using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ShelfModel
    {
        public long Id { get; set; }
        [Required]
        public required double Width { get; set; }
        [Required]
        public required double Height { get; set; }
        [Required]
        public required long LibraryId { get; set; }
        public LibraryModel? Library { get; set; }
        public List<SetModel> Sets { get; set; } = [];
    }
}
