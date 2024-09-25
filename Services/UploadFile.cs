using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ApiForecast.Services
{


    public class UploadFile
    {
        private readonly HttpClient _httpClient;

        public UploadFile(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using var content = new MultipartFormDataContent();
            using var filestream = file.OpenReadStream();
            using var filecontent = new StreamContent(filestream);
            filecontent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(filecontent, "File", file.FileName);
            content.Add(new StringContent("Forecast"), "Proyecto");

            var response = await _httpClient.PostAsync("https://storage.igrtecapi.site/api/storage/upload", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);
                return data.filePath;
            }
            else
            {
                return null;
            }
        }
    }
}