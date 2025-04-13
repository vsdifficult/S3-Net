using Microsoft.AspNetCore.Mvc;
using S3Api.Services;

namespace S3Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IS3Service _s3Service;

        public S3Controller(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromQuery] string keyName)
        {
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _s3Service.UploadFileAsync(filePath, keyName);
            return Ok("File uploaded successfully.");
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string keyName)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), keyName);
            await _s3Service.DownloadFileAsync(keyName, tempFilePath);
            return PhysicalFile(tempFilePath, "application/octet-stream", keyName);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile([FromQuery] string keyName)
        {
            await _s3Service.DeleteFileAsync(keyName);
            return Ok("File deleted successfully.");
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListFiles()
        {
            await _s3Service.ListFilesAsync();
            return Ok("Listed files.");
        }
    }
}
