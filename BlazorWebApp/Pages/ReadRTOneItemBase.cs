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
        
        protected string? InfoMsg { get; set; } = "Read All Start Message";

        // pre-build item in dev mode so we don;t have to type one in each time we test
        
        // one item from samlle data Create a list of objects - mock data initially
        //will be replaced with sql getOneItem or getSelectedItem
        // See SharedClassLib SharedModels Project for definition of rtItem Model
        // initialize a list of objects
        protected rtItem thisrtItem { get; set; } =
            new rtItem
            {
                rtId = 1001,
                rtUserName = "Cosmo-1001",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard",
                rtDateTime = DateTime.UtcNow
            };

    }

public class RTOneItem
{

}