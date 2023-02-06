using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SharedModels;


namespace BlazorWebApp.Pages
{

    public class ListItemsRTBase : ComponentBase
    {
        protected rtItem thisrtItem { get; set; } =
            new rtItem
            {
                rtId = 1001,
                rtUserName = "Cosmo",
                rtDescription = "fun time digging hole for bone",
                rtLocation = "backyard",
                rtDateTime = DateTime.UtcNow
            };
  
      
        protected void SubmitForm()
        {
            int dog = 0;
            dog++;

        }
        
    }

}