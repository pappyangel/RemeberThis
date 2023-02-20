using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;

namespace BlazorWebApp.Pages
{

    public class AddRTBase : ComponentBase
    {
        protected IBrowserFile? file;
        protected string? myAPIMessage { get; set; } = "API Return Message";
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
        protected RTModalComponent childmodal { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        protected IHttpClientFactory ClientFactory { get; set; }

        protected void DisplayBtnClicked(string _btnClicked)
        {
            myAPIMessage = _btnClicked;

        }


        protected async Task SelectedFileProcess(InputFileChangeEventArgs e)
        {
            file = e.File;
            await jsRuntime.InvokeVoidAsync("loadFileJS");
        }
        protected async Task SubmitForm()
        {
            ShowPopUp = true;


            using var content = new MultipartFormDataContent();
            var client = ClientFactory.CreateClient();

            //Add form data that bound to class into jsaon string via serialization
            string jsonString = JsonSerializer.Serialize(thisrtItem);
            var classContent = new StringContent(jsonString);
            content.Add(classContent, "classData");

            //Add file 
            var ms = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(ms);
            var streamContent = new StreamContent(ms);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

            content.Add(streamContent, "file", file.Name);

            childmodal.Open();






            // var response1 = await client.PostAsync("http://127.0.0.1:5197/RememberThis/Blaz", content);

            // switch (response1.StatusCode)
            // {
            //     case System.Net.HttpStatusCode.OK:
            //         myAPIMessage = await response1.Content.ReadAsStringAsync();
            //         break;
            //     case System.Net.HttpStatusCode.NoContent:
            //         myAPIMessage = "No content";
            //         break;
            //     case System.Net.HttpStatusCode.NotFound:
            //         myAPIMessage = "API Route not found!";
            //         break;
            //     case System.Net.HttpStatusCode.Forbidden:
            //         myAPIMessage = "Your Access to this API route is Forbidden!";
            //         break;
            //     case System.Net.HttpStatusCode.Unauthorized:
            //         myAPIMessage = "Your Access to this API route is Unauthorized!";
            //         break;
            //     default:
            //         myAPIMessage = "Unhandled Error!";
            //         break;

            // }




        }

    }

}