using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;


namespace RememberThis.Services;

public class BlobService
{
     private readonly IConfiguration _config;
     
      public BlobService(IConfiguration configuration)
        {
            // _logger = logger
            _config = configuration;

        }
    public async Task<string> GetImageSaSUrl(string fileName)
    {

        string StorageAccountName = _config["StorageConnectionString:AccountName"]!;
        string StorageAccountKey = _config["StorageConnectionString:AccountKey"]!;
        string StorageContainerName = _config["StorageConnectionString:ContainerName"]!;

        BlobContainerClient containerClient = new BlobContainerClient(StorageAccountName, StorageContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);    
        bool storageReturnCode = await blobClient.DeleteIfExistsAsync();

        return "hello";

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