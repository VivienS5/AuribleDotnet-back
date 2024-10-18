namespace Aurible.Models
{
    public class Chapter
    {
        public int idChapter { get; set; }
        public string? chapterTitle { get; set; }
        public TimeSpan[]? timecode { get; set; }
        public int page { get; set; }
        public required Book Book { get; set; }
    }
}
