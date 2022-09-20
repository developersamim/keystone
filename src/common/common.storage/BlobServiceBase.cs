using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using common.storage.Contract;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace common.storage;

public abstract class BlobServiceBase
{
    protected readonly ILogger<IBlobServiceBase> _logger;
    protected readonly BlobServiceClient _blobServiceClient;

    public BlobServiceBase(ILogger<IBlobServiceBase> logger, BlobServiceClient blobServiceClient)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
    }
    public async Task<string> UploadFormFile(string containerName, string folderPath, IFormFile formFile)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var fileExtension = new FileInfo(formFile.FileName).Extension;
        var blob = containerClient.GetBlobClient($"{folderPath}{fileExtension}".ToLowerInvariant());
        var streamReader = new StreamReader(formFile.OpenReadStream());
        var metadata = new Dictionary<string, string>
        {
            {"filename", formFile.FileName}
        };
        await blob.UploadAsync(streamReader.BaseStream, metadata: metadata, httpHeaders: new BlobHttpHeaders()
        {
            ContentType = formFile.ContentType
        });
        await blob.SetTagsAsync(metadata);

        var blobUrl = blob.Uri.AbsoluteUri;
        return blobUrl;
    }

    public async Task<string> UploadMemoryStream(string containerName, string folderPath, string fileName, MemoryStream image, string fileExtension = "jpg")
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        var blob = containerClient.GetBlobClient($"{folderPath}/{fileName}.{fileExtension}".ToLowerInvariant());
        var metadata = new Dictionary<string, string>
        {
            {"filename", fileName}
        };

        try
        {
            await blob.UploadAsync(image, metadata: metadata, httpHeaders: new BlobHttpHeaders()
            {
                ContentType = $"image/{fileExtension}"
            });
            await blob.SetTagsAsync(metadata);

        }
        catch (MagickException e)
        {
            _logger.LogError(e.Message);
        }

        var blobUrl = blob.Uri.AbsoluteUri;
        return blobUrl;
    }

    public async Task<MemoryStream> CreatThumbnail(IFormFile formFile, int thumbWidth = 64, int thumbHeight = 0)
    {
        MemoryStream thumbnailMs = new MemoryStream();
        var streamReader = new StreamReader(formFile.OpenReadStream());

        try
        {
            using (var image = new MagickImage(streamReader.BaseStream))
            {
                var size = new MagickGeometry(thumbWidth, thumbHeight);
                image.Resize(size);
                image.Write(thumbnailMs);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        thumbnailMs.Position = 0;
        return thumbnailMs;
    }
}

