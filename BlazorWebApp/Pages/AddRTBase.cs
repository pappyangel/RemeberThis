using Microsoft.AspNetCore.Components;
using SharedModels;


namespace BlazorWebApp.Pages
{

    public class AddRTBase : ComponentBase
    {
        // protected string UserName = "";
        // protected string Title = "";

        // protected rtItem.rtItem? Item {get; set;}
        protected rtItem Item { get; set; } = new rtItem{ rtId = 1001, rtUserName = "Cosmo", rtDescription = "fun time digging hole for bone",rtLocation = "backyard", rtDateTime =  DateTime.UtcNow };
                

    }

}