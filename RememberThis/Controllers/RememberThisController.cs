using System.Collections.Specialized;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SharedModels;

namespace RememberThis.Controllers;

[ApiController]
[Route("[controller]")]
public class RememberThisController : ControllerBase
{


    private readonly ILogger<RememberThisController> _logger;

    public RememberThisController(ILogger<RememberThisController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetRememberThis")]
    public ActionResult<rtItem> Get()
    {
        rtItem getItem = new rtItem { rtId = 1001, rtUserName = "Cosmo", rtDescription = "fun time digging hole for bone", rtLocation = "backyard", rtDateTime = DateTime.UtcNow };

        return Ok(getItem);
    }

    [HttpPost]
    public ActionResult<rtItem> RememberThisUpload(rtItem _rtItem)
    {
        // HttpRequest request
        HttpRequest multipartRequest = HttpContext.Request;

        // var form = await request.ReadFormAsync();

        // string? jsonData = multipartRequest.Form["jsonData"];

        //rtItem myItem = new();

        // rtItem myItem = JsonSerializer.Deserialize<rtItem>(jsonData);

        _logger.LogInformation("RememberThisUpload  {DT}",
            DateTime.UtcNow.ToLongTimeString());

        // LocalTestMethod();

        return Ok(_rtItem);

    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<rtItem> rtMulti()
    {
        HttpRequest multipartRequest = HttpContext.Request;

        StringValues rtItemJson;
        multipartRequest.Form.TryGetValue("classdata", out rtItemJson);

        rtItem? rtItemFromPost = JsonSerializer.Deserialize<rtItem>(rtItemJson[0]);

        rtItemFromPost.rtImagePath = multipartRequest.Form.Files["file"].FileName.ToString();

        return Ok(rtItemFromPost);
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<rtItem> Blaz()
    {
        HttpRequest multipartRequest = HttpContext.Request;

        StringValues rtItemJson;
        multipartRequest.Form.TryGetValue("classdata", out rtItemJson);

        rtItem? rtItemFromPost = JsonSerializer.Deserialize<rtItem>(rtItemJson[0]);

        rtItemFromPost.rtImagePath = multipartRequest.Form.Files["file"].FileName.ToString();

        return Ok("Hello from Blaz Controller");
    }


    //  public void LocalTestMethod()
    //     {       
    //         // test method demontrating calling a method in the same class file

    //     }

} // end class rememberthis