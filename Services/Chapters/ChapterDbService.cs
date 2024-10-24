using Aurible.Models;

namespace Aurible.Services
{
    public class ChapterDbService: IChapterService
    {
        private readonly ApplicationDbContext _context;
        public ChapterDbService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            _context.SaveChanges();}

        public void Add(ChapterTTS chapterTTS,Book book)
        {
            try{
                Chapter chapter = new Chapter(){
                    Book = book,
                    chapterTitle = "Chapter: "+chapterTTS.Page,
                    idBook_FK = book.idBook,
                    page = chapterTTS.Page,
                    timecode = new TimeSpan[] {TimeSpan.FromMicroseconds(chapterTTS.Timecode)}
                };
                _context.Chapters.Add(chapter);
                _context.SaveChanges();
            }catch(Exception e){
                Console.WriteLine(e.Source+":"+e.Message);
            }
        }
        public void Add(IEnumerable<Chapter> chapters)
        {
            _context.Add(chapters);
            _context.SaveChanges();
        }
        public void Add(IEnumerable<ChapterTTS> chaptersTTS,Book book)
        {
            try{
                List<Chapter> chapters = new List<Chapter>();
                foreach(var chapterTTS in chaptersTTS)
                {
                    Chapter chapter = new (){
                        Book = book,
                        chapterTitle = "Chapter: "+chapterTTS.Page,
                        idBook_FK = book.idBook,
                        page = chapterTTS.Page,
                        timecode = new TimeSpan[] {TimeSpan.FromMicroseconds(chapterTTS.Timecode)}
                    };
                    Add(chapter);
                }
            }catch(Exception e){
                Console.WriteLine(e.Source+":"+e.Message);
            }

        }
        public void Delete(int id)
        {
            var chapter =_context.Chapters.Find(id);
            if(chapter != null)
            {                
                _context.Chapters.Remove(chapter);
                _context.SaveChanges();
            }
        }
        public Chapter? Get(int id)
        {
            return _context.Chapters.Find(id);
        }

        public ChapterTTS? GetTTS(int id)
        {
            var chapter = _context.Chapters.Find(id);
            if(chapter == null) return null;
            var timecode = chapter.timecode;
            if(timecode != null) {
                return new ChapterTTS(){
                    Page = chapter.page,
                    Timecode = (ulong)timecode[0].Microseconds
                };
            }
            return null;
        } 

        public void Update(Chapter chapter)
        {
            _context.Update(chapter);
            _context.SaveChanges();
        }


    }
}
