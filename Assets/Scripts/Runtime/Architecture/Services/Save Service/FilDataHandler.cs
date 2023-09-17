using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Architecture.Services.Save_Service.Interface;

namespace Assets.Scripts.Architecture.Services.Save_Service
{
    public sealed class FileDataHandler : IFileDataHandler
    {
        public FileDataHandler() { }

        public async UniTask<string> ReadFileAsync(string filePath)
        {
            CreateDirectory(filePath);

            using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using StreamReader reader = new(sourceStream);
            StringBuilder sb = new();

            while (!reader.EndOfStream)
            {
                string line = await reader.ReadLineAsync();
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public async UniTask WriteFileAsync(string filePath, string text)
        {
            CreateDirectory(filePath);

            using FileStream destinationStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            using StreamWriter writer = new(destinationStream);
            await writer.WriteLineAsync(text);
        }

        public void DeleteFile(string path)
        {
            if (!Directory.Exists(path))
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}