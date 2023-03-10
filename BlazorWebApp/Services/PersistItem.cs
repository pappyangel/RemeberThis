using System.Net.Http.Headers;

using System.Text.Json;
using SharedModels;

namespace BlazorWebApp.Services
{
    public class PersistItem
    {
        private readonly IConfiguration _configuration = null!;

        private string PersistItemsReturnMsg = string.Empty;

        private IHttpClientFactory _ClientFactory = null!;
        //private IHttpClientFactory ClientFactory { get; set; }

        protected string apiBase { get; set; } = "http://127.0.0.1:5197";

        protected string apiRoute { get; set; } = "/RememberThis/";

        public string apiUrl { get; set; } = string.Empty;

        public PersistItem(IConfiguration configuration, IHttpClientFactory ClientFactory)
        {
            // _logger = logger
            _configuration = configuration;
            _ClientFactory = ClientFactory;

        }

        public string TestAccess()
        {
            return "You got to Test Access inside Persist Item";
        }

        public async Task<string> GetOneItemAsync(string ItemToGet)
        {

            PersistItemsReturnMsg = "GetOneItem started";

            apiUrl = apiBase + apiRoute + ItemToGet;

            
            using HttpClient apiClient = _ClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            try
            {
                var response1 = await apiClient.SendAsync(request);

                switch (response1.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        PersistItemsReturnMsg = await response1.Content.ReadAsStringAsync();
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        PersistItemsReturnMsg = "No content";
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        PersistItemsReturnMsg = "API Route not found!";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        PersistItemsReturnMsg = "Your Access to this API route is Forbidden!";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        PersistItemsReturnMsg = "Your Access to this API route is Unauthorized!";
                        break;
                    default:
                        PersistItemsReturnMsg = "Unhandled Error!";
                        break;
                }
            }
            catch (Exception Ex)
            {
                // Opps!  Did we forget to start the API?!?
                PersistItemsReturnMsg = Ex.Message;
                PersistItemsReturnMsg = "API not available";
                // throw;
            }


            return PersistItemsReturnMsg;

        }
        public async Task<string> AddItem(rtItem ItemtoAdd, MemoryStream ImageToAdd, string FileName, string FileType)
        {
            PersistItemsReturnMsg = "AddItems started";

            //long _fileSizeLimit = Config.GetValue<long>("FileSizeLimit");

            using var content = new MultipartFormDataContent();
            using HttpClient client = _ClientFactory.CreateClient();


            //Add form data that bound to class into json string via serialization
            string jsonString = JsonSerializer.Serialize(ItemtoAdd);
            var classContent = new StringContent(jsonString);
            content.Add(classContent, "classData");


            var streamContent = new StreamContent(ImageToAdd);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(FileType);
            content.Add(streamContent, "file", FileName);

            try
            {
                var response1 = await client.PostAsync("http://127.0.0.1:5197/RememberThis", content);

                switch (response1.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        PersistItemsReturnMsg = await response1.Content.ReadAsStringAsync();
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        PersistItemsReturnMsg = "No content";
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        PersistItemsReturnMsg = "API Route not found!";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        PersistItemsReturnMsg = "Your Access to this API route is Forbidden!";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        PersistItemsReturnMsg = "Your Access to this API route is Unauthorized!";
                        break;
                    default:
                        PersistItemsReturnMsg = "Unhandled Error!";
                        break;

                }

            }
            catch (Exception Ex)
            {
                // Opps!  Did we forget to start the API?!?
                PersistItemsReturnMsg = Ex.Message;
                PersistItemsReturnMsg = "API not available";
                // throw;
            }




            return PersistItemsReturnMsg;
        }


    }   // end class 



} // End namespace