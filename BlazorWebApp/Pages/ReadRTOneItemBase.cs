using BlazorWebApp.Services;
using Microsoft.AspNetCore.Components;
using SharedModels;


namespace BlazorWebApp.Pages;

public class ReadRTOneItemBase : ComponentBase
{
    protected long myTicks = DateTime.Now.Ticks;
    protected string myTicksString = string.Empty;
    protected string? InfoMsg { get; set; } = "Read All Start Message";

    protected rtItem thisrtItem { get; set; } = new();
    //protected IBrowserFile file = null!;    
    protected string? ChildModalBody { get; set; } = string.Empty;

    [Inject]
    protected PersistItem _PersistItem { get; set; } = null!;



    protected async override void OnInitialized()
    {
        InfoMsg = "OnInitialized";
        //Replace below with call to API
        thisrtItem.rtId = 1001;
        thisrtItem.rtUserName = "Cosmo-1001";
        thisrtItem.rtDescription = "fun time digging hole for bone";
        thisrtItem.rtLocation = "backyard";
        thisrtItem.rtDateTime = DateTime.UtcNow;
        thisrtItem.rtImagePath = "./Images/BabaMan.jpg";
        thisrtItem.rtImagePath = "./Images/Cosmo-sox.png";


        InfoMsg =  await _PersistItem.GetOneItemAsync("1001");



    }

}



