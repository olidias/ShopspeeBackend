using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopspeeBackend.Services;

namespace ShopspeeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechRecognitionController : ControllerBase
    {
        private readonly ILogger<SpeechRecognitionController> _logger;

        public SpeechRecognitionController(ILogger<SpeechRecognitionController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(byte[] snippetData)
        {
            Console.WriteLine($"Got request post with length {snippetData?.Length}");
            if (snippetData == null || snippetData.Length < 1) return new StatusCodeResult(400);
            string result;
            try
            {
                Console.WriteLine($"Received bytes: {snippetData.Length}");
                // var snippet = Convert.FromBase64String(base64snippet);
                result = await new DeepSpeechRecognitionService().RecogniseVoiceSnippet(snippetData);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unsuccessful request: {ex.Message}");
                return new StatusCodeResult(404);
            }

	        return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
	        return Ok(new DeepSpeechRecognitionService().RecogniseVoiceSnippet(null));
        }
    }
}
