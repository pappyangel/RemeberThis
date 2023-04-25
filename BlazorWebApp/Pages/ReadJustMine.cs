using BlazorWebApp.Services;
using Microsoft.AspNetCore.Components;
using SharedModels;


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



    protected async override Task OnInitializedAsync()
    {
        DebugMsg = "OnInitializedXX";
        //Replace below with call to API
        thisrtItem.rtId = 1001;
        thisrtItem.rtUserObjectId = "Cosmo-1001";
        thisrtItem.rtDescription = "fun time digging hole for bone";
        thisrtItem.rtLocation = "backyard";
        thisrtItem.rtDateTime = DateTime.UtcNow;
      
        if (rand.NextDouble() >= 0.5)
            thisrtItem.rtImagePath = "./Images/BabaMan.jpg";
        else
            thisrtItem.rtImagePath = "./Images/Cosmo-sox.png";
        string userTrue = "a387ff55-c87d-4ed6-a8d6-0e6cb3be3443";
        string userFalse = "64ecf344-71fe-4901-8722-b716f64f58bd";

        ItemsList = await _ItemService.GetAllItemsAsync(userTrue);


        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);
        // ItemsList?.Add(thisrtItem);


       DebugMsg = "back from API";


    }

}



