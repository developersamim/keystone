using System;
using Azure.Storage.Blobs;
using common.storage;
using common.storage.Contract;
using Microsoft.Extensions.Logging;
using user.application.Contracts.Infrastructure;

namespace user.infrastructure.Service.BlobStorage;

public class BlobService : BlobServiceBase, IBlobService
{
    public BlobService(ILogger<IBlobServiceBase> logger, BlobServiceClient blobServiceClient)
        : base(logger, blobServiceClient) { }
}

