using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace rtWebApp.Pages;

public class CallAPIModel : PageModel
{
    private readonly ILogger<CallAPIModel> _logger;

    public CallAPIModel(ILogger<CallAPIModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
