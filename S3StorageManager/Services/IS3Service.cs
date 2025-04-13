using System.Threading.Tasks;

namespace S3StorageManager.Services
{
    public interface IS3Service
    {
        Task UploadFileAsync(string filePath, string keyName);
        Task DownloadFileAsync(string keyName, string destinationPath);
        Task DeleteFileAsync(string keyName);
        Task ListFilesAsync();
    }
}
