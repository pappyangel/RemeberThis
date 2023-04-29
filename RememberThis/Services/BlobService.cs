using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using SharedModels;


namespace RememberThis.Services;

public class BlobService
{
    private readonly IConfiguration _config;

    public BlobService(IConfiguration configuration)
    {
        // _logger = logger
        _config = configuration;

    }
    public List<rtItem> GetImageSaSUrl(List<rtItem> Items)
    {
        string StorageAccountName = _config["StorageConnectionString:AccountName"]!;
        string StorageAccountKey = _config["StorageConnectionString:AccountKey"]!;
        string StorageContainerName = _config["StorageConnectionString:ContainerName"]!;

        Uri blobContainerUri = new(string.Format("https://{0}.blob.core.windows.net/{1}", StorageAccountName, StorageContainerName));
        StorageSharedKeyCredential storageSharedKeyCredential = new(StorageAccountName, StorageAccountKey);
        BlobContainerClient blobContainerClient = new(blobContainerUri, storageSharedKeyCredential);

        foreach (var item in Items)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(item.rtImagePath);

            // Check whether this BlobClient object has been authorized with Shared Key.
            if (blobClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                
                item.ImageUrl = sasUri.ToString();
            }           

        }

        return Items;

    }
    public async Task<string> DeleteFromAzureStorageAsync(string fileName)
    {
        string methodReturnValue = string.Empty;
        string StorageConnectionString = _config["AZURE_STORAGE_CONNECTION_STRING"]!;
        string ImageContainer = _config["ImageContainer"]!;

        BlobContainerClient containerClient = new BlobContainerClient(StorageConnectionString, ImageContainer);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        try
        {
            bool storageReturnCode = await blobClient.DeleteIfExistsAsync();
            methodReturnValue = "DeleteBlobSuccess";
        }
        catch (Exception Ex)
        {
            methodReturnValue = Ex.Message;
            methodReturnValue = "ERROR-Blob Delete";
            // throw;
        }

        return methodReturnValue;

    }  // end of write to azure storage

    public async Task<string> WritetoAzureStorageAsync(MemoryStream _ms, string filename)
    {
        string methodReturnValue = string.Empty;
        string StorageConnectionStringOLD = _config["AZURE_STORAGE_CONNECTION_STRING"]!;
        //account name, account string
        string StorageAccountName = _config["StorageConnectionString:AccountName"]!;
        string StorageAccountKey = _config["StorageConnectionString:AccountKey"]!;
        string StorageContainerName = _config["StorageConnectionString:ContainerName"]!;
        //string StorageConnectionString = $"DefaultEndpointsProtocol=https;AccountName={StorageAccountName};AccountKey={StorageAccountKey};EndpointSuffix=core.windows.net";
        string StorageConnectionString = $"DefaultEndpointsProtocol=https;AccountName={StorageAccountName};AccountKey={StorageAccountKey}";

        //DefaultEndpointsProtocol=https;AccountName=;AccountKey=;EndpointSuffix=core.windows.net"
        Boolean OverWrite = true;

        string trustedExtension = Path.GetExtension(filename).ToLowerInvariant();
        // string trustedNewFileName = Guid.NewGuid().ToString();
        string trustedNewFileName = string.Format("{0:MM-dd-yyyy-H:mm:ss.fff}", DateTime.UtcNow);
        string trustedFileNameAndExt = trustedNewFileName + trustedExtension;

        methodReturnValue = trustedFileNameAndExt;

        BlobContainerClient containerClient = new BlobContainerClient(StorageConnectionString, StorageContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(trustedFileNameAndExt);

        _ms.Position = 0;

        try
        {
            await blobClient.UploadAsync(_ms, OverWrite);

        }
        catch (Exception Ex)
        {
            methodReturnValue = Ex.Message;
            methodReturnValue = "ERROR-Storage";
            // throw;
        }

        return methodReturnValue;

    }  // end of write to azure storage

}