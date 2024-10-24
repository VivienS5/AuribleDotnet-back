using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurible.Models 
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idBook { get; set; }
        public string? title { get; set; }
        public string? resume { get; set; }
        public string? coverURL { get; set; }
        public string? audioPath { get; set; }
        public int? maxPage { get; set; }
        public string? author { get; set; }

        public ICollection<Chapter> Chapters { get; set; } = [];
    }
}
