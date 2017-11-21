using System.IO;
using System.Threading.Tasks;

namespace HelpYouBudget.NETStandard.Data
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream, bool showPdf);

    }


    public interface ISaveWindowsPhone
    {
        Task Save(string filename, string contentType, MemoryStream stream);
    }

    public interface IAndroidVersionDependencyService
    {
        int GetAndroidVersion();
    }


    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }

    public interface ITextToSpeech
    {
        void Speak(string text);
        void Stop();

    }
}