using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Architecture.Services.Save_Service.Interface
{
    public interface IFileDataHandler
    {
        void DeleteFile(string path);
        UniTask<string> ReadFileAsync(string filePath);
        UniTask WriteFileAsync(string filePath, string text);
    }
}