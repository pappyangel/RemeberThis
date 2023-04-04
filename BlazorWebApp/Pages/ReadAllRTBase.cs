using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
// using System.Net.Http;
// using System.Text.Json;
// using System.Net.Http.Headers;

namespace BlazorWebApp.Pages;

    public class ReadAllRTBase : ComponentBase
    {
        // [Inject]
        // protected IConfiguration Config { get; set; }

        // [Inject]
        // protected IJSRuntime jsRuntime { get; set; } = null!;

        // [Inject]
        // protected IHttpClientFactory ClientFactory { get; set; } = null!;

        // [Inject]
        // protected IConfiguration Config { get; set; } = null!;
        
        protected string? DebugMsg { get; set; } = "Read All Start Message";

        // pre-build item in dev mode so we don;t have to type one in each time we test
        
        // Create a list of objects - mock data initially
        // See SharedClassLib SharedModels Project for definition of rtItem Model
        // initialize a list of objects
        public List<rtItem> rtItemList = new List<rtItem>
        {
            new rtItem
            {   rtId = 1001,
                rtUserObjectId = "Cosmo1",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard1",
                rtDateTime = DateTime.UtcNow
            },
            new rtItem
            {   rtId = 1002,
                rtUserObjectId = "Cosmo2",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard2",
                rtDateTime = DateTime.UtcNow
            },
            new rtItem
            {   rtId = 1003,
                rtUserObjectId = "Cosmo3",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard3",
                rtDateTime = DateTime.UtcNow
            },
            new rtItem
            {   rtId = 1004,
                rtUserObjectId = "Cosmo4",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard4",
                rtDateTime = DateTime.UtcNow
            }
        };

    }







