using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
using System.Net.Http;

namespace BlazorWebApp.Pages
{

    public class AddRTBase : ComponentBase
    {
        protected IBrowserFile? file;
        protected string? myAPIMessage { get; set; }
        protected string apiBase { get; set; } = "http://127.0.0.1:5026";

        protected string apiRoute { get; set; } = "/RememberThis/rtMulti";

        public string apiUrl { get; set; } = "";
        protected bool ShowPopUp { get; set; } = false;
        protected rtItem thisrtItem { get; set; } =
            new rtItem
            {
                rtId = 1001,
                rtUserName = "Cosmo",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard",
                rtDateTime = DateTime.UtcNow
            };
        // protected async Task SubmitForm()

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        protected IHttpClientFactory ClientFactory { get; set; }

      
        protected async Task SelectedFileProcess(InputFileChangeEventArgs e)
        {            
            file = e.File;
            await jsRuntime.InvokeVoidAsync("loadFileJS");
        }
        protected async Task SubmitForm()
        {
            ShowPopUp = true;
            int dog = 0;
            
            using var content = new MultipartFormDataContent();
            var client = ClientFactory.CreateClient();
            var response =  await client.PostAsync("http://127.0.0.1:5197/RememberThis/Blaz",  content);
            
            dog++;

        }
        
    }

}