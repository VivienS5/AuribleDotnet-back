using Aurible.Models;
using Aurible.Services.TTSServices;

namespace Aurible.Services
{
    public delegate void SynthesisProgression(int page, int totalPage);
    public delegate void SynthesisCompleted(List<ChapterTTS> chapterTTs,int idbook);

    public class TTSService
    {
        private readonly ConvertTTSService _convertTTSService;
        private readonly PreProcessingService _preProcessingService;
        public TTSService(){
            _convertTTSService = new ConvertTTSService();
            _preProcessingService = new PreProcessingService();
        }
        public async Task UploadBook(string file,int idbook,SynthesisCompleted SynthesisCompleted){
            List<ChapterTTS>? chapterTTs = await _convertTTSService.StartSynthesizeAudio(_preProcessingService.GetDocument(file), idbook.ToString());
            if(chapterTTs != null){
                SynthesisCompleted.Invoke(chapterTTs,idbook);
                Console.WriteLine("Synthesis completed");
            }else{
                Console.WriteLine("Synthesis failed");
            }
        }
    }
    
}