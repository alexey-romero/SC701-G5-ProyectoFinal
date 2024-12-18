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
        Task<string> GetNewsByQuerie(string querie, string apiKey);
    }

    public class ApiNewsService : IApiNewsService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetNewsByQuerie(string querie, string apiKey)
        {
            string baseUrl = "https://newsapi.org/v2/everything";

            // Obtener la fecha del día anterior en formato correcto
            string previousDate = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            string sortBy = "popularity";

            // Codificar los parámetros para evitar caracteres problemáticos en la URL
            string encodedQuerie = Uri.EscapeDataString(querie);
            string encodedApiKey = Uri.EscapeDataString(apiKey);

            // Construcción de la URL
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

