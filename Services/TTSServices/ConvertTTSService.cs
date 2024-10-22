using System.Text;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Aurible.Services.TTSServices
{
    public class ConvertTTSService
    {
        private readonly string speechKey = "EYuvdMB2VCE50nDHwuhyMWeN6prRXIW9zsQx105wf1o4eX4Wcjk1JQQJ99AJAC5RqLJXJ3w3AAAYACOGvT56";
        private readonly string serviceRegion = "westeurope";
        private readonly string speechSynthesisVoiceName = "fr-FR-DeniseNeural";
        private readonly string speechSynthesisLanguage = "fr-FR";
        private readonly string audioOutput = "C:\\Users\\Guillaume\\Documents\\Informatique\\AuribleDotnet-back\\audio";
        public ConvertTTSService(){

        }
        public async Task StartSynthesizeAudio(Dictionary<int,string>? books,string title){
            if(books == null){
                Console.WriteLine("Le livre est vide");
                return;
            }
            string ssml = GenerateSSML(books);
            await SynthesizeAudioAsync(ssml, title);
        }
        private SpeechSynthesizer config(string title){
            var speechConfig= SpeechConfig.FromSubscription(speechKey, serviceRegion);
            speechConfig.SpeechSynthesisLanguage = speechSynthesisLanguage;
            speechConfig.SpeechSynthesisVoiceName = speechSynthesisVoiceName;
            var absolutePath = Path.GetFullPath(Path.Combine(audioOutput, title + ".wav"));
            using var audioConfig = AudioConfig.FromWavFileOutput(absolutePath);
            var speechSynthesiszer = new SpeechSynthesizer(speechConfig, audioConfig);
            return speechSynthesiszer;
        }
        async private Task SynthesizeAudioAsync(string texte,string title) {
            Console.WriteLine("Synthèse audio en cours...");
            Console.WriteLine(speechKey);
            var speechSynthesiszer = config(title);
            if(speechSynthesiszer == null){
                Console.WriteLine("Erreur lors de la configuration du service de synthèse audio");
                return;
            }
            speechSynthesiszer.BookmarkReached  += (s,e) => {
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
    }
}
