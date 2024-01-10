using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CodeHub.Service
{
    public class UploadService
    {

        public async Task<string> UploadImageAsync(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                byte[] fileData;
                using (var binaryReader = new BinaryReader(imageFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(imageFile.ContentLength);
                }

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Client-ID 38a01a79c306c2a");
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new ByteArrayContent(fileData), "image", "image.png");

                HttpResponseMessage response = await httpClient.PostAsync("https://api.imgur.com/3/image", form);

                if (response.IsSuccessStatusCode)
                {
                    string imgurResponse = await response.Content.ReadAsStringAsync();
                    dynamic imgurJson = JObject.Parse(imgurResponse);
                    return imgurJson.data.link;
                }
                else
                {
                    return "Upload failed with status code: " + response.StatusCode;
                }
            }
            return null;
        }
    }
}