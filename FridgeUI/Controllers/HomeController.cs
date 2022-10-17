using AutoMapper;
using FridgeUI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(HttpClient client, ILogger<HomeController> logger) : base(client)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            string errorMessage = error.Message;
            if (error is HttpRequestException httpException)
            {
                switch (httpException.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        {
                        Notify("Unauthorized, redirect to login", NotificationType.warning);
                        return RedirectToAction("Login", "Auth");
                        }

                    case HttpStatusCode.Forbidden:
                        errorMessage = "Code 403, forbidden access";
                        break;

                    case HttpStatusCode.BadRequest:
                        errorMessage = "Code: 400, BadRequest";
                        break;

                    case HttpStatusCode.InternalServerError:
                        errorMessage = "Code 500, InternalServerError";
                        break;
                    case HttpStatusCode.Conflict:
                        errorMessage = "Code 409, Same record already exists in database";
                        break;
                    case HttpStatusCode.NotFound:
                        errorMessage = "Code: 404, NotFound";
                        break;
                }
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = errorMessage
            });
        }
        public IActionResult Index()
        {
            string token;
            Request.Cookies.TryGetValue("token", out token);
            if (!token.IsNullOrEmpty())
            {
                var handler = new JwtSecurityTokenHandler();
                var Token = handler.ReadJwtToken(token);
                User.AddIdentity(new ClaimsIdentity(Token.Claims));
                
            }
            return View();
        }
        public IActionResult CallSp()
        {
            var content = SendHttpRequest(HttpMethod.Get, "fridgeproducts/changezeroquantity", null);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }


    }
}
