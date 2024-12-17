using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    public interface IApiPexelsService
    {
        Task<string> GetImagesByQuerie(string querie, string apiKey);
    }

    public class ApiPexelsService : IApiPexelsService
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<string> GetImagesByQuerie(string querie, string apiKey)
        {
            string baseUrl = "https://api.pexels.com/v1/search";

            string url = $"{baseUrl}?query={querie}&per_page=1";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", apiKey);

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject images = JObject.Parse(responseBody);

            return images.ToString();

        }

    }
}
