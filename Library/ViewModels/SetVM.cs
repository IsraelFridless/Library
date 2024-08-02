using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class SetVM
    {
        public long ShelfId { get; set; }
        public required string SetName { get; set; }
    }
}
