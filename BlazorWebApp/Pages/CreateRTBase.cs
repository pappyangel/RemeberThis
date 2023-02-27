using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using BlazorWebApp.Services;

namespace BlazorWebApp.Pages
{

    public class CreateRTBase : ComponentBase
    {
        protected IBrowserFile file = null!;        
        protected string? InfoMsg { get; set; } = "API Return Message";
        protected string? ChildModalBody { get; set; } = string.Empty;
        protected string apiBase { get; set; } = "http://127.0.0.1:5026";

        protected string apiRoute { get; set; } = "/RememberThis/rtMulti";

        public string apiUrl { get; set; } = string.Empty;
        protected bool ShowPopUp { get; set; } = false;
        
        //pre-build item in dev mode so we don;t have to type one in each time we test
        protected rtItem thisrtItem { get; set; } =
            new rtItem
            {
                rtId = 1001,
                rtUserName = "Cosmo",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard",
                rtDateTime = DateTime.UtcNow
            };
        
        protected RTModalComponent childmodal { get; set; } = null!;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; } = null!;

        [Inject]
        protected IHttpClientFactory ClientFactory { get; set; } = null!;

        [Inject]
        protected IConfiguration Config { get; set; } = null!;

        [Inject]
        protected PersistItem _PersistItem { get; set; } = null!;

        protected void DisplayBtnClicked(string _btnClicked)
        {
            InfoMsg = _btnClicked;

        }

        protected async Task SelectedFileProcess(InputFileChangeEventArgs e)
        {
            long _fileSizeLimit = Config.GetValue<long>("FileSizeLimit");
            
             if (!((e.File.Size > 0) && (e.File.Size < _fileSizeLimit)))
            {
                ChildModalBody = "File size invalid";
                childmodal.Open();
                await jsRuntime.InvokeVoidAsync("ResetFilePicker");
                
            }
            else
            {
                // file size is good so save to class variable and update preview on screen
                file = e.File;            
                await jsRuntime.InvokeVoidAsync("loadFileJS");
            }
        }
        protected async Task SubmitForm()
        {
            
            // PersistItem persistItem = new(Config);
            
            string PersistReturnMsg = string.Empty;

            PersistReturnMsg = _PersistItem.TestAccess();

            var ms = new MemoryStream();
            await file.OpenReadStream(1024 * 1024 * 10).CopyToAsync(ms);
            ms.Position = 0;

            PersistReturnMsg = await _PersistItem.AddItem(thisrtItem, ms);

            ms.Position = 0;
            
            // start of code block to move

            // long _fileSizeLimit = Config.GetValue<long>("FileSizeLimit");

            // using var content = new MultipartFormDataContent();
            // var client = ClientFactory.CreateClient();

            // //Add form data that bound to class into jsaon string via serialization
            // string jsonString = JsonSerializer.Serialize(thisrtItem);
            // var classContent = new StringContent(jsonString);
            // content.Add(classContent, "classData");

            // //Add file             
            // if (!((file.Size > 0) && (file.Size < _fileSizeLimit)))
            // {
            //     InfoMsg = "File size invalid";
            // }
            // else
            // {
            //     //var ms = new MemoryStream();
            //     await file.OpenReadStream(1024 * 1024 * 10).CopyToAsync(ms);
            //     ms.Position = 0;
            //     var streamContent = new StreamContent(ms);
            //     streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

            //     content.Add(streamContent, "file", file.Name);

            //     try
            //     {
            //         var response1 = await client.PostAsync("http://127.0.0.1:5197/RememberThis", content);

            //         switch (response1.StatusCode)
            //         {
            //             case System.Net.HttpStatusCode.OK:
            //                 InfoMsg = await response1.Content.ReadAsStringAsync();
            //                 break;
            //             case System.Net.HttpStatusCode.NoContent:
            //                 InfoMsg = "No content";
            //                 break;
            //             case System.Net.HttpStatusCode.NotFound:
            //                 InfoMsg = "API Route not found!";
            //                 break;
            //             case System.Net.HttpStatusCode.Forbidden:
            //                 InfoMsg = "Your Access to this API route is Forbidden!";
            //                 break;
            //             case System.Net.HttpStatusCode.Unauthorized:
            //                 InfoMsg = "Your Access to this API route is Unauthorized!";
            //                 break;
            //             default:
            //                 InfoMsg = "Unhandled Error!";
            //                 break;

            //         }

            //     }
            //     catch (Exception Ex)
            //     {
            //         // Opps!  Did we forget to start the API?!?
            //         InfoMsg = "API not available";
            //         // throw;
            //     }


                // end of code block to move
            

        }

    }

}