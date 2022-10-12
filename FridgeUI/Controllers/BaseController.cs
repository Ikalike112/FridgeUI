using FridgeUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    public class BaseController : Controller
    {
        public HttpClient Client { get; private set;}
        public readonly JsonSerializerOptions Options;
        public BaseController(HttpClient client)
        {
            client.BaseAddress = new System.Uri("https://localhost:5001/api/");
            client.Timeout = new System.TimeSpan(0, 0, 30);
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Client = client;
        }
        public async Task<string> SendHttpRequest(HttpMethod httpMethod, string requestUri, object? body)
        {
            var request = new HttpRequestMessage(httpMethod, requestUri);
            if (body != null)
            {
            request.Content = JsonContent.Create(body);
            }
            var response = await Client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public void Notify(string message, string title = "Sweet Alert Toastr Demo",
                                    NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = "sweetAlert"//toastr
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
    }
}
