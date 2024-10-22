using System;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Aurible.Services.TTSServices
{
    public class ConvertTTSService
    {
        private readonly string speechKey = "";
        private readonly string serviceRegion = "westeurope";
        private readonly string speechSynthesisVoiceName = "fr-FR-DeniseNeural";
        private readonly string speechSynthesisLanguage = "fr-FR";
        private readonly string audioOutput = "C:\\Users\\Guillaume\\Documents\\Informatique\\AuribleDotnet-back\\audio";
        public ConvertTTSService(){

        }
        public async Task StartSynthesizeAudio(Dictionary<int,string>? book,string title){
            if(book == null){
                Console.WriteLine("Le livre est vide");
                return;
            }
            var tasks = book.Select(async page => await SynthesizeAudioAsync(page.Value,title));
            await Task.WhenAll(tasks);
        }
        private SpeechSynthesizer config(string title){
             var speechConfig= SpeechConfig.FromSubscription(speechKey, serviceRegion);
            speechConfig.SpeechSynthesisLanguage = speechSynthesisLanguage;
            speechConfig.SpeechSynthesisVoiceName = speechSynthesisVoiceName;

            var absolutePath = Path.GetFullPath(Path.Combine(audioOutput, title + ".wav"));
            using var audioConfig = AudioConfig.FromWavFileOutput(absolutePath);
            using var speechSynthesiszer = new SpeechSynthesizer(speechConfig, audioConfig);
            return speechSynthesiszer;
        }
        async private Task SynthesizeAudioAsync(string texte,string title) {
            Console.WriteLine("Synthèse audio en cours...");
            Console.WriteLine(speechKey);
            var speechSynthesiszer = config(title);
            speechSynthesiszer.SynthesisStarted += (s,e) => {
                
            };
            speechSynthesiszer.SynthesisCompleted += (s,e) => {
            };
            speechSynthesiszer.SynthesisCanceled += (s,e) => {
            };
            SpeechSynthesisResult speechSynthesisResult  = await speechSynthesiszer.SpeakTextAsync("Je suis un test qui parle.");

        }
        public void OutputSpeechSynthesis(SpeechSynthesisResult speechSynthesisResult,string text){
            switch (speechSynthesisResult.Reason)
        {
            case ResultReason.SynthesizingAudioCompleted:
                Console.WriteLine($"Speech synthesized for text: [{text}]");
                break;
            case ResultReason.Canceled:
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
            default:
                break;
        }
        } 
    }
}
