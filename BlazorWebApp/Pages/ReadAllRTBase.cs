using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;

namespace BlazorWebApp.Pages
{


    public class ReadAllRTBase : ComponentBase
    {
        [Inject]
        protected IConfiguration Config { get; set; }
        protected string? InfoMsg { get; set; } = "Read All Start Message";


    }

}
