using System.Threading.Tasks;

namespace ShopspeeBackend.Services
{
    public interface ISpeechRecognitionService
    {
        Task<string> RecogniseVoiceSnippet(byte[] snippetData);
    }
}
