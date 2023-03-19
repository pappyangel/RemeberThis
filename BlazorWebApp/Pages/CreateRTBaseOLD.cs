using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using BlazorWebApp.Services;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorWebApp.Pages
{

    public class CreateRTBaseOLD : ComponentBase
    {
        protected IBrowserFile file = null!;

        protected RTModalComponent? childmodal { get; set; } = null!;
        protected string? DebugMsg { get; set; } = "API Return Message";
        protected string apiBase { get; set; } = "http://127.0.0.1:5026";

        protected string apiRoute { get; set; } = "/RememberThis/rtMulti";

        public string apiUrl { get; set; } = string.Empty;
        protected bool ShowPopUp { get; set; } = false;

        //pre-build item in dev mode so we don't have to type one in each time we test
        protected rtItem thisrtItem { get; set; } =
            new rtItem
            {
                rtId = 1001,
                rtUserName = "Cosmo",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard",
                rtDateTime = DateTime.UtcNow
            };
        protected EditContext? rtItemEditContext;
        protected string ChildModalBody { get; set; } = string.Empty;


        [Inject]
        protected IHttpClientFactory ClientFactory { get; set; } = null!;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; } = null!;

        [Inject]
        protected ItemService _ItemService { get; set; } = null!;

         [Inject]
        protected IConfiguration Config { get; set; } = null!;

        protected override void OnInitialized()
        {
            rtItemEditContext = new(thisrtItem);
        }
        protected async Task SelectedFileProcess(InputFileChangeEventArgs e)
        {
            long _fileSizeLimit = Config.GetValue<long>("FileSizeLimit");

            if (!((e.File.Size > 0) && (e.File.Size < _fileSizeLimit)))
            {
                ChildModalBody = "File size invalid";
                childmodal!.Open();
                await jsRuntime.InvokeVoidAsync("ResetFilePicker");

            }
            else
            {
                // file size is good so save to class variable and update preview on screen
                file = e.File;
                await jsRuntime.InvokeVoidAsync("loadFileJS");
            }
        }

        protected void DisplayBtnClicked(string _btnClicked)
        {
            DebugMsg = _btnClicked;

        }

        protected async Task ConfirmInternalNavigation(LocationChangingContext context)
        {
            var confirmed = await jsRuntime.InvokeAsync<bool>("confirm", "Discard your changes?");

            if (!confirmed)
            {
                context.PreventNavigation();
            }
        }


        protected async Task SubmitForm()
        {
            string PersistReturnMsg = string.Empty;

            // need to add code to handle no image selected
            var ms = new MemoryStream();
            await file.OpenReadStream(1024 * 1024 * 10).CopyToAsync(ms);
            ms.Position = 0;

            PersistReturnMsg = await _ItemService.AddItem(thisrtItem, ms, file.Name, file.ContentType);

            DebugMsg = PersistReturnMsg;


        }

    }

}