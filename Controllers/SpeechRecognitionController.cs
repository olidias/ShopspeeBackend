using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopspeeBackend.Services;
using ShopspeeBackend.Model;

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
        public async Task<ActionResult<string>> Post([FromBody] VoiceSnippet snippetData)
        {
            Console.WriteLine($"Got request post with length {snippetData?.Data?.Length}");
            if (snippetData == null || snippetData.Data == null || snippetData.Data.Length < 1) return new StatusCodeResult(400);
            string result;
            Console.WriteLine($"Hex string of param: {BitConverter.ToString(snippetData.Data).Replace("-","")}");
            try
            {
                Console.WriteLine($"Received bytes: {snippetData.Data.Length}");
                // var snippet = Convert.FromBase64String(snippetData.Data);
                result = await new DeepSpeechRecognitionService().RecogniseVoiceSnippet(snippetData.Data);
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
