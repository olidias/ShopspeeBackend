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
        public async Task<ActionResult<string>> Post(string base64snippet)
        {
            Console.WriteLine("Got request post");
            if(string.IsNullOrEmpty(base64snippet)) return new StatusCodeResult(400);
            string result;
            try
            {
                Console.WriteLine($"Received base 64 encoded bytes: {base64snippet}");
                var snippet = Convert.FromBase64String(base64snippet);
                result = await new DeepSpeechRecognitionService().RecogniseVoiceSnippet(snippet);
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
