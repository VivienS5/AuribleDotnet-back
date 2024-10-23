using System.Text;
using Aurible.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Aurible.Services.TTSServices
{
    public class ConvertTTSService
    {
        private readonly string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY") ?? "YOUR_SPEECH_KEY";
        private readonly string serviceRegion = "westeurope";
        private readonly string speechSynthesisVoiceName = "fr-FR-DeniseNeural";
        private readonly string speechSynthesisLanguage = "fr-FR";
        private readonly string audioOutput = "audio";
        private readonly List<ChapterTTS> chapters = [];
        public ConvertTTSService(){

        }
        public async Task<List<ChapterTTS>?> StartSynthesizeAudio(Dictionary<int,string>? books,string title){
            if(books == null){
                Console.WriteLine("Le livre est vide");
                return null;
            }
            string ssml = GenerateSSML(books);
            await SynthesizeAudioAsync(ssml, title);
            return chapters;
        }
        private SpeechSynthesizer config(string title){
            var speechConfig= SpeechConfig.FromSubscription(speechKey, serviceRegion);
            speechConfig.SpeechSynthesisLanguage = speechSynthesisLanguage;
            speechConfig.SpeechSynthesisVoiceName = speechSynthesisVoiceName;
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio24Khz48KBitRateMonoMp3);
            var absolutePath = Path.GetFullPath(Path.Combine(audioOutput, title + ".mp3"));
            using var audioConfig = AudioConfig.FromWavFileOutput(absolutePath);
            var speechSynthesiszer = new SpeechSynthesizer(speechConfig, audioConfig);
            return speechSynthesiszer;
        }
        async private Task SynthesizeAudioAsync(string texte,string title) {
            Console.WriteLine("Synthèse audio en cours...");
            Console.WriteLine(speechKey);
            var speechSynthesiszer = config(title);
            if(speechSynthesiszer == null || speechKey == "YOUR_SPEECH_KEY"){
                Console.WriteLine("Erreur lors de la configuration du service de synthèse audio");
                return;
            }
            speechSynthesiszer.BookmarkReached  += (s,e) => {
                chapters.Add(AddChapters(e.AudioOffset, e.Text));
                Console.WriteLine($"Bookmark reached: {e.Text}, Audio offset: {e.AudioOffset / 10000} ms");
            };
            speechSynthesiszer.SynthesisStarted += (s,e) => {
                Console.WriteLine("Synthèse audio démarrée");
            };
            speechSynthesiszer.SynthesisCompleted += (s,e) => {
                Console.WriteLine("Synthèse audio terminée");
            };
            speechSynthesiszer.SynthesisCanceled += (s,e) => {
                Console.WriteLine("Synthèse audio annulée");
                Console.WriteLine(e.Result.Reason);
            };
            await speechSynthesiszer.SpeakSsmlAsync(texte);

        }
        static string GenerateSSML(Dictionary<int, string> books){
            StringBuilder ssmlBuilder = new ();
            ssmlBuilder.AppendLine("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='https://www.w3.org/2001/mstts' xml:lang='string'>");
            ssmlBuilder.AppendLine("<voice name='fr-FR-DeniseNeural'>");
            foreach (var page in books)
            {
                ssmlBuilder.AppendLine($"<mstts:express-as role='YoungAdultFemale' style='calm' >");
                ssmlBuilder.AppendLine(page.Value);
                ssmlBuilder.AppendLine($"<bookmark mark='{page.Key}'/>");
                ssmlBuilder.AppendLine("<break time='0.5s'/>");
                ssmlBuilder.AppendLine("</mstts:express-as>");
            }
            ssmlBuilder.AppendLine("</voice>");
            ssmlBuilder.AppendLine("</speak>");
            return ssmlBuilder.ToString();
        }
        static ChapterTTS AddChapters(ulong time, string bookmark){
            return new () {
                Timecode = time / 10000, // Convert offset to seconds
                Page = int.Parse(bookmark)
            };
        }
    }
}
