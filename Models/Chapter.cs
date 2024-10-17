namespace Aurible.Models
{
    public class Chapter
    {
        public int IdChapter { get; set; }
        public string chapterTitle { get; set; }
        public TimeSpan[] timecode { get; set; }
        public int page { get; set; }
        public int idBook_FK { get; set; }
        public Book Book { get; set; }
    }
}
