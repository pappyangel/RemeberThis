using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RememberThis.Models;

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
        rtItem getItem = new rtItem{ rtId = 1001, rtUser = "Cosmo", rtDescription = "fun time digging hole for bone",rtLocation = "backyard", rtDate =  DateTime.UtcNow };
      
        return Ok(getItem);
    }

    [HttpPost]
    public ActionResult<rtItem> RememberThisUpload (rtItem _rtItem)
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

    //  public void LocalTestMethod()
    //     {       
    //         // test method demontrating calling a method in the same class file

    //     }

} // end class rememberthis