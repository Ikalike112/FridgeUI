using AutoMapper;
using FridgeUI.Models.Auth;
using FridgeUI.Models.FridgeModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    public class AuthController : BaseController
    {
        public IMapper _mapper { get; set; }
        public AuthController(HttpClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            DeleteUserCookies();
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            DeleteUserCookies();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelDto model)
        {
            var content = await SendHttpRequest(HttpMethod.Post, "Auth/Register", model);
            var user = JsonConvert.DeserializeObject<ApplicationUserDto>(content);
            SetUserCookies(user);
            SetUserPrincipal(user.JwtToken);
            return RedirectToAction("Account", user);
        }
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var content = await SendHttpRequest(HttpMethod.Get, "Auth",null);
            var user = JsonConvert.DeserializeObject<ApplicationUserDto>(content);
            if (user == null)
                return RedirectToAction("Login");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto model)
        {
            var content = await SendHttpRequest(HttpMethod.Post, "Auth/Login", model);
            var user = JsonConvert.DeserializeObject<ApplicationUserDto>(content);
            SetUserCookies(user);
            SetUserPrincipal(user.JwtToken);
            return RedirectToAction("Account",user);
        }
        public async Task<IActionResult> Logout()
        {
            DeleteUserCookies();
            return RedirectToAction("Index", "Home");
        }
        private void SetUserCookies(ApplicationUserDto profile)
        {
            Response.Cookies.Append("token", profile.JwtToken);
            Response.Cookies.Append("email", profile.Email);
        }
        private void SetUserPrincipal(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var Token = handler.ReadJwtToken(token);
            User.AddIdentity(new ClaimsIdentity(Token.Claims));     
        }
        private void DeleteUserCookies()
        {
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("email");
        }
    }
}
