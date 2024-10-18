namespace Aurible.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? resume { get; set; }
        public string? coverURL { get; set; }
        public string? audioPath { get; set; }
        public int? maxPage { get; set; }
        public string? author { get; set; }

        public ICollection<Chapter>? Chapters { get; set; }
    }
}
