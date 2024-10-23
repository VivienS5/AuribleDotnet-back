namespace Aurible.Models
{
    public class Chapter
    {
        public int idChapter { get; set; }
        public string? chapterTitle { get; set; }
        public TimeSpan[]? timecode { get; set; }
        public int page { get; set; }
        public int idBook_FK { get; set; }
        public required Book Book { get; set; }
    }
    public class ChapterTTS
    {
        public required ulong Timecode { get; set; }
        public required int Page { get; set; }
    }
}
