using System.Net.Http.Headers;

using System.Text.Json;
using SharedModels;

namespace BlazorWebApp.Services
{
    public class PersistItem
    {
        private readonly IConfiguration _configuration = null!;
        
        private IHttpClientFactory _ClientFactory = null!;
        //private IHttpClientFactory ClientFactory { get; set; }

        public PersistItem(IConfiguration configuration, IHttpClientFactory ClientFactory)
        {
            // _logger = logger
            _configuration = configuration;
            _ClientFactory = ClientFactory;

        }

        public string TestAccess( )
        {
            return "You got to Test Access inside Persist Item";
        }
        public async Task<string> AddItem(rtItem ItemtoAdd, MemoryStream ImageToAdd, string FileName, string FileType)
        {
            string AddItemsReturnMsg = "AddItems started";
            string InfoMsg = string.Empty;

            //long _fileSizeLimit = Config.GetValue<long>("FileSizeLimit");

            using var content = new MultipartFormDataContent();
            using HttpClient client = _ClientFactory.CreateClient();
            //var client = ClientFactory.CreateClient();

            //Add form data that bound to class into jsaon string via serialization
            string jsonString = JsonSerializer.Serialize(ItemtoAdd);
            var classContent = new StringContent(jsonString);
            content.Add(classContent, "classData");

            
            var streamContent = new StreamContent(ImageToAdd);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("Need File Type");
            content.Add(streamContent, "file", "Need File Name");

                try
                {
                    var response1 = await client.PostAsync("http://127.0.0.1:5197/RememberThis", content);

                    switch (response1.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            InfoMsg = await response1.Content.ReadAsStringAsync();
                            break;
                        case System.Net.HttpStatusCode.NoContent:
                            InfoMsg = "No content";
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            InfoMsg = "API Route not found!";
                            break;
                        case System.Net.HttpStatusCode.Forbidden:
                            InfoMsg = "Your Access to this API route is Forbidden!";
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                            InfoMsg = "Your Access to this API route is Unauthorized!";
                            break;
                        default:
                            InfoMsg = "Unhandled Error!";
                            break;

                    }

                }
                catch (Exception Ex)
                {
                    // Opps!  Did we forget to start the API?!?
                    InfoMsg = Ex.Message;
                    InfoMsg = "API not available";
                    // throw;
                }




            
            
            AddItemsReturnMsg = "AddItems completed";

            return AddItemsReturnMsg;
        }


    }   // end class 



} // End namespace