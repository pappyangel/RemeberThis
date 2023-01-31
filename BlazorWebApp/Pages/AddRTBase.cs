using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;



namespace BlazorWebApp.Pages
{

    public class AddRTBase : ComponentBase
    {
        protected IBrowserFile? file;
        
        protected string base64Image { get; set; } = string.Empty;

        protected string? myAPIMessage { get; set; }
        protected string apiBase { get; set; } = "http://127.0.0.1:5026";

        protected string apiRoute { get; set; } = "/RememberThis/rtMulti";

        public string apiUrl { get; set; } = "";
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
        protected void SubmitForm()
        {
            int dog=0;
            dog++;

        }
         protected async Task LoadFile(InputFileChangeEventArgs e)
        {
           var format = "image/png";
           var resizedImage = await e.File.RequestImageFileAsync(format, 200,200);           
           var buffer = new byte[resizedImage.Size];
           await resizedImage.OpenReadStream().ReadAsync(buffer);
           
        //    var buffer = new byte[e.File.Size];
        //    await e.File.OpenReadStream().ReadAsync(buffer);

           base64Image = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
           //file.ContentType = "image/png";
           //file = e.File;
           

        }

    }

}