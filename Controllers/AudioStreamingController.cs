using Aurible.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Authorize]
    [Route("audio")]
    public class AudioStreamingController:ControllerBase
    {
        private readonly AudioStreamingService _audioStreaminService;
        public AudioStreamingController(AudioStreamingService audioStreaminService){
            _audioStreaminService = audioStreaminService;
        }
        [HttpGet("{bookId}")]
        public IActionResult StreamAudio(int bookId)
        {
            var audioStream = _audioStreaminService.GetAudioStreamingById(bookId);
            if (audioStream == null)
            {
                return NotFound(new { message = "Audio not found" });
            }

            return audioStream;
        }
        [HttpGet("chapters/{bookId}")]
        public IActionResult ListChaptersAudio(int bookId)
        {
            var chapters = _audioStreaminService.GetChaptersByBookId(bookId);
            if (chapters == null)
            {
                return NotFound();
            }
            return Ok(chapters);
        }
    }
}