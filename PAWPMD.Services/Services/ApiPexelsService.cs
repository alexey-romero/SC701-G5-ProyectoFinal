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

        /// <summary>
        /// Asynchronously retrieves images related to the specified query from the Pexels API using the provided API key.
        /// The method constructs the request URL, makes an HTTP GET request to the API, and returns the image results as a JSON string.
        /// </summary>
        /// <param name="querie">The search query for which images are to be retrieved (e.g., a keyword like "nature" or "city").</param>
        /// <param name="apiKey">The API key required to authenticate the request to the Pexels service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the image search results.</returns>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or the response status is not successful.</exception>

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
