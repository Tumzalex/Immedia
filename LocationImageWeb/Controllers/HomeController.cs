using LocationImageWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocationImageWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var client = _clientFactory.CreateClient("image-api");

            StringContent stringContent = new StringContent("{}",Encoding.UTF8, mediaType: "application/json");
            
            var response = await client.PostAsync("/api/LocationImages/locations", stringContent);

            var list = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(await response.Content.ReadAsStreamAsync());

            return View(list);
        }

        public async Task<IActionResult> GetLocationImagesAsync(string location)
        {
            dynamic imageLocationQuery = new ExpandoObject();
            imageLocationQuery.Location = location;

            var client = _clientFactory.CreateClient("image-api");

            StringContent stringContent = new StringContent(JsonSerializer.Serialize(imageLocationQuery), Encoding.UTF8, mediaType: "application/json");

            var response = await client.PostAsync("/api/LocationImages/images", stringContent);

            var list = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(await response.Content.ReadAsStreamAsync());

            return View(new LocationImagesViewModel { LocationName = location, ImageLinks = list });
        }

        public async Task<IActionResult> GetImageDetailsAsync(string url)
        {
            dynamic imageLocationQuery = new ExpandoObject();
            imageLocationQuery.Uri = new Uri(url);

            var client = _clientFactory.CreateClient("image-api");

            StringContent stringContent = new StringContent(JsonSerializer.Serialize(imageLocationQuery), Encoding.UTF8, mediaType: "application/json");

            var response = await client.PostAsync("/api/LocationImages/image/details", stringContent);

            var imageDetails = await JsonSerializer.DeserializeAsync<string>(await response.Content.ReadAsStreamAsync());

            return View(imageDetails);
        }
    }
}
