namespace Aurible.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Resume { get; set; }
        public string? CoverURL { get; set; }
        public string? AudioPath { get; set; }
        public int? maxPage { get; set; }
        public string? Author { get; set; }
    }
}
