using System.Collections.Specialized;
using System.Text.Json;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RememberThis.DB;
using SharedModels;


namespace RememberThis.Controllers;

[ApiController]
// [Route("[controller]")]
[Route("RememberThis")]
public class RememberThisController : ControllerBase
{
    private readonly ILogger<RememberThisController> _logger;
    private readonly IConfiguration _config;
    private string apiReturnMsg = "Controller Start";
    private string[] permittedExtensions = new string[] { ".gif", ".png", ".jpg", ".jpeg" };
    public RememberThisController(ILogger<RememberThisController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    // [HttpGet(Name = "GetRememberThis")]
    [HttpGet]
    public ActionResult<rtItem> GetAll()
    {
        rtItem getItem = new rtItem { rtId = 1001, rtUserName = "Cosmo", rtDescription = "fun time digging hole for bone", rtLocation = "backyard", rtDateTime = DateTime.UtcNow };

        return Ok(getItem);
    }

    // [HttpGet("id/{id:int}")]
    [HttpGet("id/{itemId}")]
    public ActionResult<rtItem> GetOne(int itemId)
    {
        rtItem getItemId = new rtItem { 
            rtId = itemId, 
            rtUserName = "Cosmo", 
            rtDescription = "fun time digging hole for bone", 
            rtLocation = "backyard", 
            rtImagePath = "Martini.jpg",
            rtDateTime = DateTime.UtcNow };        

        string GetOneapiReturnMsg = "You sent this to Get One End Point: " + itemId.ToString();

        return Ok(getItemId);
    }

    [HttpPost]
    public async Task<ActionResult> RememberThisUpload()
    {

        HttpRequest multipartRequest = HttpContext.Request;

        // get class data from key value pair out of multi part
        StringValues rtItemJson;
        multipartRequest.Form.TryGetValue("classdata", out rtItemJson);
        rtItem? rtItemFromPost = JsonSerializer.Deserialize<rtItem>(rtItemJson[0]!);
        rtItemFromPost!.rtImagePath = multipartRequest.Form.Files["file"]?.FileName.ToString();

        var form = await multipartRequest.ReadFormAsync();
        var formFile = form.Files["file"];
        string unsafeFileNameAndExt = formFile!.FileName;

        await using var stream = formFile.OpenReadStream();
        using var ms = new MemoryStream();

        await stream.CopyToAsync(ms);

        if (IsValidFileExtensionAndSignature(formFile.FileName, ms))
        {
            string StorageErrorOrFileName = await WritetoAzureStorage(ms, unsafeFileNameAndExt);

            if (StorageErrorOrFileName.StartsWith("ERROR"))
            {
                // do error with storage process
                apiReturnMsg = StorageErrorOrFileName;
            }
            else
            {
                // write to sql process
                rtItemFromPost.rtImagePath = StorageErrorOrFileName;
                SqlDb sqlDb = new(_config);
                int rowsAffected = await sqlDb.InsertrtItem(rtItemFromPost);

                if ((rowsAffected == 0))
                {   
                    // roll back azure storage write                    
                    apiReturnMsg = "SQL Insert failed, Storage rolled back";
                }
                else
                {
                    apiReturnMsg += " - SQL Insert Success";
                }

            }

            _logger.LogInformation("Received request to add this item: {rtItemFromPost}", rtItemFromPost);

        }

        //return $"Row(s) inserted were: {rowsAffected}";
        return Ok(apiReturnMsg);
    }

    private async Task<string> WritetoAzureStorage(MemoryStream _ms, string filename)
    {
        apiReturnMsg = "StorageStart";
        string methodReturnValue = string.Empty;
        string StorageConnectionString = _config["AZURE_STORAGE_CONNECTION_STRING"]!;
        string ImageContainer = _config["ImageContainer"]!;
        Boolean OverWrite = true;

        string trustedExtension = Path.GetExtension(filename).ToLowerInvariant();
        // string trustedNewFileName = Guid.NewGuid().ToString();
        string trustedNewFileName = string.Format("{0:MM-dd-yyyy-H:mm:ss.fff}", DateTime.UtcNow);
        string trustedFileNameAndExt = trustedNewFileName + trustedExtension;

        methodReturnValue = trustedFileNameAndExt;

        BlobContainerClient containerClient = new BlobContainerClient(StorageConnectionString, ImageContainer);
        BlobClient blobClient = containerClient.GetBlobClient(trustedFileNameAndExt);

        _ms.Position = 0;

        try
        {
            await blobClient.UploadAsync(_ms, OverWrite);
            apiReturnMsg = "StorageWriteSuccess";
        }
        catch (Exception Ex)
        {
            methodReturnValue = Ex.Message;
            methodReturnValue = "ERROR-Storage";
            // throw;
        }


        return methodReturnValue;

    }  // end of write to azure storage
    public bool IsValidFileExtensionAndSignature(string fileName, MemoryStream streamParam)
    {

        // might need to go somehwere higher in stack
        apiReturnMsg = "Validation Method Start";
        apiReturnMsg = "file check start";

        MemoryStream data = new MemoryStream();
        streamParam.Position = 0;
        streamParam.CopyTo(data);

        // this is generally checked by the file upload control itself - but we can double check it here
        if (data == null || data.Length == 0)
        {
            apiReturnMsg = "file empty";
            return false;
        }

        var filenameonly = Path.GetFileNameWithoutExtension(fileName);
        if (string.IsNullOrEmpty(filenameonly))
        {
            apiReturnMsg = "file name not valid";
            return false;
        }

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
        {
            apiReturnMsg = "file extension not valid";
            return false;
        }

        data.Position = 0;

        using (var reader = new BinaryReader(data))
        {
            var signatures = _fileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            bool fileSigCorrect = signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));

            apiReturnMsg = fileSigCorrect ? "file check good" : "file signiture invalid";

            return fileSigCorrect;

        }

    } // End IsValidFileExtensionAndSignature

    private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            }
        };


} // end class rememberthis