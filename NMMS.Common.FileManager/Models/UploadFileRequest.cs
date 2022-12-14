using Microsoft.AspNetCore.Http;

namespace NMMS.Common.FileManager.Models
{
    public class UploadFileRequest
    {
        public IFormFile File { get; set; }
        public Guid Id { get; set; }
    }
}
