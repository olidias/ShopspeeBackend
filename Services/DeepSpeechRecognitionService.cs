using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using NAudio.Wave;

namespace ShopspeeBackend.Services
{
    public class DeepSpeechRecognitionService : ISpeechRecognitionService
    {
        private const string MODEL_PATH = "/home/oldi/deepspeech-data/deepspeech-0.7.1-models.pbmm";
        private const string SCORER_PATH = "/home/oldi/deepspeech-data/deepspeech-0.7.1-models.scorer";

        public async Task<string> RecogniseVoiceSnippet(byte[] snippetData)
        {
			var audioPath = $"/home/oldi/deepspeech-data/examples/audio/{DateTime.Now.ToString("yyyyMMddHHmmss")}.wav";
			
			if (snippetData == null || snippetData.Length == 0)
			{
				throw new ArgumentException("No snippet data");
			}

			File.WriteAllBytes(audioPath, snippetData);

            try
            {
				var cmd = $"deepspeech --model {MODEL_PATH} --scorer {SCORER_PATH} --audio {audioPath}";
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{cmd}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                string result = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();
				Console.WriteLine(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
