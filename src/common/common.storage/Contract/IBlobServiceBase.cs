using Microsoft.AspNetCore.Http;

namespace common.storage.Contract;

public interface IBlobServiceBase
{
    Task<string> UploadFormFile(string containerName, string folderPath, IFormFile formFile);
    Task<string> UploadMemoryStream(string containerName, string folderPath, string fileName, MemoryStream image, string fileExtension = "jpg");
    Task<MemoryStream> CreatThumbnail(IFormFile formFile, int thumbWidth = 50, int thumbHeight = 40);
}

