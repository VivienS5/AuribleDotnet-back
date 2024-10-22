using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Aurible.Services.TTSServices
{
    public class PreProcessingService
    {
        public void GetDocument(string path){
            try{
                using PdfDocument document = PdfDocument.Open(path);
                var result =SplitPage(document.GetPages());
                foreach (var page in result)
                {
                    Console.WriteLine("Page Number: "+page.Key);
                    Console.WriteLine(page.Value);
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public Dictionary<int,string> SplitPage(IEnumerable<Page> pages){
            var pageDictionary = new Dictionary<int, string>();
            foreach (var page in pages)
            {
                List<string> words=RemoveHeaderAndFooter(page);
                words = RemoveLink(words);
                words = RemoveSpecialCharacter(words);

                pageDictionary.Add(page.Number, Merge(words));
            }
            return pageDictionary;
        }
        public string Merge(List<string> words){
            return string.Join(" ", words);
        }
        public List<string> RemoveHeaderAndFooter(Page page){
            var headerThresholdY = GetHeaderPostion(page);
            var footerThresholdY = 100;
            var wordsWithoutHeaderFooter = new List<string>();
            foreach (var word in page.GetWords())
            {
                if (word.BoundingBox.BottomLeft.Y < headerThresholdY && word.BoundingBox.BottomLeft.Y > footerThresholdY)
                    {
                        wordsWithoutHeaderFooter.Add(word.Text);
                    }
            }
            return wordsWithoutHeaderFooter;
        }
        public List<string> RemoveLink(List<string> words){
            List<string> filtredWords = [];
            foreach (var word in words)
            {
                if(!IsLink(word)){
                    filtredWords.Add(word);
                }
            }
            return filtredWords;
        }
        static List<string> RemoveSpecialCharacter(List<string> words)
        {
            const string specialCharacterPattern = @"[^a-zA-Z0-9À-ÿ\s'.-]";
            List<string> filtredWords = [];
            foreach (var word in words)
            {
                string resultText = Regex.Replace(word, specialCharacterPattern, string.Empty);
                filtredWords.Add(resultText);
            }
            return  filtredWords;
             
        }
        static int GetHeaderPostion(Page page){
            double pageHeight = page.Height;
            var headerElement = page.Letters.Where(letter => letter.GlyphRectangle.BottomRight.Y > pageHeight * 0.8)
            .ToList();
            if(headerElement.Count != 0)
            {
                return (int)headerElement.First().GlyphRectangle.BottomRight.Y;
            }
            return 0;
        }
        static int GetFooterPosition(Page page){
            double pageHeight = page.Height;
            var headerElement = page.Letters.Where(letter => letter.GlyphRectangle.TopLeft.Y > pageHeight * 0.3)
            .ToList();
            if(headerElement.Count != 0)
            {
                return (int)headerElement.First().GlyphRectangle.TopLeft.Y;
            }
            return 0;
        }
        static bool IsLink(string text)
        {
            string linkPattern = @"(http[s]?:\/\/|www\.|\.com|\.org|\.net|\.edu)";
            return Regex.IsMatch(text, linkPattern, RegexOptions.IgnoreCase);
        }
        
    }
}