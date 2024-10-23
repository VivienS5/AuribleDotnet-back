using Aurible.Models;

namespace Aurible.Services
{
    public interface IChapterService
    {
        void Add(Chapter chapter);
        void Add(ChapterTTS chapterTTS,Book book);
        void Add(IEnumerable<Chapter> chapters);
        void Add(IEnumerable<ChapterTTS> chapters,Book book);
        void Update(Chapter chapter);
        Chapter? Get(int id);
        ChapterTTS? GetTTS(int id);
        void Delete(int id);
    }
}