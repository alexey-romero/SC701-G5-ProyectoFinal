using Newtonsoft.Json.Linq;
using PAWPMD.Architecture.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    public interface IApiNinjaService
    {
        /// <summary>
        /// Asynchronously retrieves detailed information about a specified city from the API Ninjas City API using the provided city name and API key.
        /// The method constructs the request URL, makes an HTTP GET request to the API, and returns the city details as a JSON string if found. 
        /// If no city details are found, an exception is thrown.
        /// </summary>
        /// <param name="city">The name of the city for which details are to be retrieved.</param>
        /// <param name="apiKey">The API key required to authenticate the request to the API Ninjas service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the city details.</returns>
        /// <exception cref="PAWPMDException">Thrown if no city details are found for the provided city name. The exception message will specify that the city details were not found.</exception>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or the response status is not successful.</exception>

        Task<string> GetCityDetailsAsync(string city, string apiKey);
    }

    public class ApiNinjaService : IApiNinjaService
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Asynchronously retrieves detailed information about a specified city from the API Ninjas City API using the provided city name and API key.
        /// The method constructs the request URL, makes an HTTP GET request to the API, and returns the city details as a JSON string if found. 
        /// If no city details are found, an exception is thrown.
        /// </summary>
        /// <param name="city">The name of the city for which details are to be retrieved.</param>
        /// <param name="apiKey">The API key required to authenticate the request to the API Ninjas service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the city details.</returns>
        /// <exception cref="PAWPMDException">Thrown if no city details are found for the provided city name. The exception message will specify that the city details were not found.</exception>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or the response status is not successful.</exception>

        public async Task<string> GetCityDetailsAsync(string city, string apiKey)
        {
            string url = $"https://api.api-ninjas.com/v1/city?name={city}";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JArray cityDetailsArray = JArray.Parse(responseBody);

            if (cityDetailsArray.Count == 0)
            {
                throw new PAWPMDException("City details not found.");
            }

            JObject cityDetails = (JObject)cityDetailsArray[0];

            return cityDetails.ToString();
        }
    }
}
