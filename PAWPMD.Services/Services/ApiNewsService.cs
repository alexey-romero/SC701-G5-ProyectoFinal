using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    public interface IApiNewsService
    {

        /// <summary>
        /// Asynchronously retrieves news articles from the NewsAPI based on a query and API key. The method constructs a URL with the provided query and API key, and fetches news articles that match the query from the previous day.
        /// </summary>
        /// <param name="querie">The search query used to filter the news articles. This can be any keyword or phrase you want to search for in the news articles.</param>
        /// <param name="apiKey">The API key required to authenticate the request to NewsAPI. This key should be obtained from the NewsAPI service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the news articles matching the query from the previous day.</returns>
        /// <exception cref="Exception">Thrown if the HTTP request is unsuccessful. The exception message contains the status code and error response from the NewsAPI.</exception>

        Task<string> GetNewsByQuerie(string querie, string apiKey);
    }

    public class ApiNewsService : IApiNewsService
    {
        private static readonly HttpClient client = new HttpClient();


        /// <summary>
        /// Asynchronously retrieves news articles from the NewsAPI based on a query and API key. The method constructs a URL with the provided query and API key, and fetches news articles that match the query from the previous day.
        /// </summary>
        /// <param name="querie">The search query used to filter the news articles. This can be any keyword or phrase you want to search for in the news articles.</param>
        /// <param name="apiKey">The API key required to authenticate the request to NewsAPI. This key should be obtained from the NewsAPI service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the news articles matching the query from the previous day.</returns>
        /// <exception cref="Exception">Thrown if the HTTP request is unsuccessful. The exception message contains the status code and error response from the NewsAPI.</exception>

        public async Task<string> GetNewsByQuerie(string querie, string apiKey)
        {
            string baseUrl = "https://newsapi.org/v2/everything";

            string previousDate = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            string sortBy = "popularity";

            string encodedQuerie = Uri.EscapeDataString(querie);
            string encodedApiKey = Uri.EscapeDataString(apiKey);

            string url = $"{baseUrl}?q={encodedQuerie}&from={previousDate}&to={previousDate}&sortBy={sortBy}&apiKey={encodedApiKey}";

            client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");

            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode}, Response: {errorResponse}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject news = JObject.Parse(responseBody);

            return news.ToString();
        }
    }
}

