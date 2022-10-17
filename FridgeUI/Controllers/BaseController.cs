using FridgeUI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Claims;
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
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            Client = client;

        }
        public async Task<string> SendHttpRequest(HttpMethod httpMethod, string requestUri, object? body)
        {
            var request = new HttpRequestMessage(httpMethod, requestUri);
            string token = "";
            Request.Cookies.TryGetValue("token", out token);
            if (!token.IsNullOrEmpty())
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var handler = new JwtSecurityTokenHandler();
                var Token = handler.ReadJwtToken(token);
                User.AddIdentity(new ClaimsIdentity(Token.Claims));
            }
            if (body != null)
            {
            request.Content = JsonContent.Create(body);
            }

            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public void Notify(string message, NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = "sweetAlert"
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
        
    }
}
