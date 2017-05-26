using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace WebApp.Services
{
    public class GeoCoordsService
    {
        private readonly object _logger;
        private readonly IConfigurationRoot _config;

        public GeoCoordsService(ILogger<GeoCoordsService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoCoordsResult> GetCoordsAsync(string name)
        {
            GeoCoordsResult result = new GeoCoordsResult
            {
                Success = false,
                Message = "Failed to get coordinates"
            };

            string apiKey = _config["Keys:BingKey"];
            string encodedName = WebUtility.UrlEncode(name);
            string url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            JObject results = JObject.Parse(json);
            JToken resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                string confidence = (string) resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    JToken coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double) coords[0];
                    result.Longitude = (double) coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
