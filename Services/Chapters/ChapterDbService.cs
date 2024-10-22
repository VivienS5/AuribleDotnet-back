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
                    idChapter = chapterTTS.Page,
                    page = chapterTTS.Page,
                    timecode = new TimeSpan(0,0,chapterTTS.Timecode)
                };
                _context.Chapters.Add(chapter);
                _context.SaveChanges();
            }catch(Exception e){
                Console.WriteLine(e.Message);
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
                List<Chapter> chapters = [];
                foreach(var chapterTTS in chaptersTTS)
                {
                    Chapter chapter = new (){
                        Book = book,
                        chapterTitle = "Chapter: "+chapterTTS.Page,
                        idBook_FK = book.idBook,
                        idChapter = chapterTTS.Page,
                        page = chapterTTS.Page,
                        timecode = new TimeSpan(0,0,chapterTTS.Timecode)
                    };
                    chapters.Add(chapter);
                }
                Add(chapters);
            }catch(Exception e){
                Console.WriteLine(e.Message);
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
                    Timecode = (int)timecode.Value.TotalSeconds
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
