using System.Collections.Specialized;
using System.Text.Json;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RememberThis.Services;
using SharedModels;


namespace RememberThis.Controllers;

[ApiController]
// [Route("[controller]")]
[Route("RememberThis")]
public class RememberThisController : ControllerBase
{
    private readonly ILogger<RememberThisController> _logger;
    private readonly IConfiguration _config;
    private readonly BlobStorage _BlobStorage;
    private readonly SqlDb _SqlDb;
    private readonly ImageService _ImageService;

    private string apiReturnMsg = "Controller Start";

    public RememberThisController(ILogger<RememberThisController> logger, IConfiguration config,
            BlobStorage BlobStorage, SqlDb SqlDb, ImageService ImageService)
    {
        _logger = logger;
        _config = config;
        _BlobStorage = BlobStorage;
        _SqlDb = SqlDb;
        _ImageService = ImageService;

    }

    // [HttpGet(Name = "GetRememberThis")]
    [HttpGet]
    public ActionResult<rtItem> GetAll()
    {
        rtItem getItem = new rtItem { rtId = 1001, rtUserObjectId = "Cosmo", rtDescription = "fun time digging hole for bone", rtLocation = "backyard", rtDateTime = DateTime.UtcNow };

        return Ok(getItem);
    }

    // [HttpGet("id/{id:int}")]
    [HttpGet("id/{itemId}")]
    public ActionResult<rtItem> GetOne(int itemId)
    {
        rtItem getItemId = new rtItem
        {
            rtId = itemId,
            rtUserObjectId = "Cosmo",
            rtDescription = "fun time digging hole for bone",
            rtLocation = "backyard",
            rtImagePath = "Martini.jpg",
            rtDateTime = DateTime.UtcNow
        };

        string GetOneapiReturnMsg = "You sent this to Get One End Point: " + itemId.ToString();

        return Ok(getItemId);
    }

    //[HttpDelete("id/{id:int}")]
    [HttpDelete]
    public async Task<ActionResult<string>> DeleteItem(rtItem ItemtoDelete)
    {
        int RowsAffected = await _SqlDb.DeleteItem(ItemtoDelete);
        string DeleteReturnMsg = (RowsAffected == 1) ? "Item Deleted from SQL" : "Error in SQL Delete";

        string deleteStorageMsg = await _BlobStorage.DeleteFromAzureStorageAsync(ItemtoDelete.rtImagePath!);
        if (deleteStorageMsg == "DeleteBlobSuccess")
            DeleteReturnMsg += " - Item Deleted from Storage";
        else
            DeleteReturnMsg += " and Storage Delete";

        return DeleteReturnMsg;

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

        if (_ImageService.IsValidFileExtensionAndSignature(formFile.FileName, ms))
        {
            string StorageErrorOrFileName = await _BlobStorage.WritetoAzureStorageAsync(ms, unsafeFileNameAndExt);

            if (StorageErrorOrFileName.StartsWith("ERROR"))
            {
                // do error with storage process
                apiReturnMsg = StorageErrorOrFileName;
            }
            else
            {
                // write to sql process
                rtItemFromPost.rtImagePath = StorageErrorOrFileName;
                apiReturnMsg = "StorageWriteSuccess";

                int rowsAffected = await _SqlDb.InsertrtItem(rtItemFromPost);

                if ((rowsAffected == 1))
                {
                    apiReturnMsg += " - SQL Insert Success";
                }
                else
                {
                    // roll back azure storage write        
                    string deleteStorageMsg = await _BlobStorage.DeleteFromAzureStorageAsync(StorageErrorOrFileName);
                    if (deleteStorageMsg == "DeleteBlobSuccess")
                        apiReturnMsg = "SQL Insert failed, Storage roll-back success";
                    else
                        apiReturnMsg = "SQL Insert failed, Storage roll-back failed";

                    // throw;                   

                }

            }

            _logger.LogInformation("Received request to add this item: {rtItemFromPost}", rtItemFromPost);

        }

        //return $"Row(s) inserted were: {rowsAffected}";
        return Ok(apiReturnMsg);
    }





} // end class rememberthis