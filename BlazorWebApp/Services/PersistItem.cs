namespace BlazorWebApp.Services
{
    public class PersistItem
    {
        private readonly IConfiguration _configuration;
        public PersistItem(IConfiguration configuration)
        {
            // _logger = logger
            _configuration = configuration;

        }

        public string TestAccess()
        {
            return "You got to Test Access inside Persist Item";
        }
        public string AddItem()
        {
            
            
            
            
            return "You got to AddItem";
        }


    }   // end class 



} // End namespace