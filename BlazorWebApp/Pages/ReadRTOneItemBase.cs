using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
// using System.Net.Http;
// using System.Text.Json;
// using System.Net.Http.Headers;

namespace BlazorWebApp.Pages;

public class ReadRTOneItemBase : ComponentBase
{
    protected long myTicks = DateTime.Now.Ticks;
    protected string myTicksString = string.Empty;
    protected string? InfoMsg { get; set; } = "Read All Start Message";
    
    protected rtItem thisrtItem { get; set; } = new();    
        


    protected override void OnInitialized()
    {

        //Replace below with call to API
        thisrtItem.rtId = 1001;
        thisrtItem.rtUserName = "Cosmo-1001";
        thisrtItem.rtDescription = "fun time digging hole for bone";
        thisrtItem.rtLocation = "backyard";
        thisrtItem.rtDateTime = DateTime.UtcNow;
        thisrtItem.rtImagePath = "./Images/BabaMan.jpg";
        thisrtItem.rtImagePath = "./Images/Cosmo-sox.png";        


    }

}



