using Microsoft.AspNetCore.Mvc;

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
    public string Get()
    {
        return "RememberThis Controller test successful";
    }
    [HttpPost]
    public string RememberThisUpload()
    {
        HttpRequest multipartRequest = HttpContext.Request;

        string? jsonData = multipartRequest.Form["jsonData"];

        // myItem = JsonSerializer.Deserialize<Item>(jsonData);

        _logger.LogInformation("RememberThisUpload  {DT}",
            DateTime.UtcNow.ToLongTimeString());
        
        // LocalTestMethod();

        return $"File Upload Endpoint Success with: {jsonData}";

    }

    //  public void LocalTestMethod()
    //     {       
    //         // test method demontrating calling a method in the same class file

    //     }

} // end class rememberthis