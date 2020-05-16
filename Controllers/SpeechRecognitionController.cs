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
        public async Task<ActionResult<string>> PostSpeechSnippet(byte[] snippet)
        {
	        return Ok(await new DeepSpeechRecognitionService().RecogniseVoiceSnippet(snippet));
        }

        [HttpGet]
        public IActionResult Get()
        {
	        return Ok(new DeepSpeechRecognitionService().RecogniseVoiceSnippet(null));
        }
    }
}
