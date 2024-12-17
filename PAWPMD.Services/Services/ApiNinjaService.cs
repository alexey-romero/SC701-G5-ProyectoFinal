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
        Task<string> GetCityDetailsAsync(string city, string apiKey);
    }

    public class ApiNinjaService : IApiNinjaService
    {
        private static readonly HttpClient client = new HttpClient();

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
