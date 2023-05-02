using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using BlazorWebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWebApp.Pages
{

    public class CreateRTBase : ComponentBase
    {
        protected IBrowserFile? file = null!;

        protected OneItem? childOneItem { get; set; } = null!;


        protected string apiBase { get; set; } = "http://127.0.0.1:5026";

        protected string apiRoute { get; set; } = "/RememberThis/rtMulti";

        public string apiUrl { get; set; } = string.Empty;
        protected bool ShowPopUp { get; set; } = false;


        //pre-build item in dev mode so we don't have to type one in each time we test
        protected rtItem? thisrtItem { get; set; } = null!;

        protected string createCardTitle { get; set; } = "Add a memory you want to save and share later!";

        protected EditContext? rtItemEditContext;
        protected string? DebugMsg { get; set; } = "API Return Message";

        [Inject]
        protected IHttpClientFactory ClientFactory { get; set; } = null!;

        [Inject]
        protected AuthenticationStateProvider authenticationStateProvider { get; set; } = null!;

        [Inject]
        protected ItemService _ItemService { get; set; } = null!;

        [Inject]
        protected NavigationManager NavManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            thisrtItem = new rtItem
            {
                rtId = 0,
                rtUserObjectId = string.Empty,
                rtDescription = string.Empty,
                rtLocation = string.Empty,
                rtDateTime = DateTime.UtcNow
            };

            // rtItemEditContext = new(thisrtItem);
        }

        protected async Task SubmitFormAsync(IBrowserFile fileFromChild)
        {
            string PersistReturnMsg = string.Empty;

            file = fileFromChild;

            var authState = await authenticationStateProvider
                                    .GetAuthenticationStateAsync();
            var user = authState.User;

            thisrtItem!.rtUserObjectId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //thisrtItem.rtDateTime = DateTime.UtcNow;

            // need to add code to handle no image selected
            var ms = new MemoryStream();
            await file.OpenReadStream(1024 * 1024 * 10).CopyToAsync(ms);
            ms.Position = 0;

            PersistReturnMsg = await _ItemService.AddItem(thisrtItem!, ms, file.Name, file.ContentType);

            //check return code and display error or redirect to summary
            NavManager.NavigateTo("/ReadJustMine");

            DebugMsg = PersistReturnMsg;


        }

    }

}