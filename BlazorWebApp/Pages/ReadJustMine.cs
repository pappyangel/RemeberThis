using BlazorWebApp.Services;
using Microsoft.AspNetCore.Components;
using SharedModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace BlazorWebApp.Pages;

public class ReadJustMineBase : ComponentBase
{
    protected long myTicks = DateTime.Now.Ticks;
    protected Random rand = new Random();
    protected string myTicksString = string.Empty;
    protected string? DebugMsg { get; set; } = "Read All Start Message";

    protected rtItem thisrtItem { get; set; } = new();
    protected List<rtItem>? ItemsList { get; set; } = new();


    protected string? ChildModalBody { get; set; } = string.Empty;

    [Inject]
    protected ItemService _ItemService { get; set; } = null!;
    [Inject]
    protected AuthenticationStateProvider authenticationStateProvider { get; set; } = null!;




    protected async override Task OnInitializedAsync()
    {
        DebugMsg = "OnInitializedXX";

        var authState = await authenticationStateProvider
                                      .GetAuthenticationStateAsync();
        var user = authState.User;

        //thisrtItem!.rtUserObjectId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        string? userObjectId = String.Empty;
        userObjectId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        // string userTrue = "a387ff55-c87d-4ed6-a8d6-0e6cb3be3443";
        // string userFalse = "64ecf344-71fe-4901-8722-b716f64f58bd";

        ItemsList = await _ItemService.GetAllItemsAsync(userObjectId!);



        DebugMsg = "back from API";


    }

}



