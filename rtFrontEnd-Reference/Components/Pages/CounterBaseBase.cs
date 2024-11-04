using Microsoft.AspNetCore.Components;

namespace rtFrontEnd.Components.Pages
{
    public class CounterBaseBase : ComponentBase
    {
        protected int bCount = 0;

        protected void IncrementCount()
        {
            bCount++;
        }
    }
}
