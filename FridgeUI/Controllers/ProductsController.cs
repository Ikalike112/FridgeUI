using AutoMapper;
using FridgeUI.ActionFilters;
using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FridgeUI.Models.ProductModels;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FridgeUI.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : BaseController
    {
        public IMapper _mapper { get; set; }
        public ProductsController(HttpClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var content = await SendHttpRequest(HttpMethod.Get, "products", null);
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(content);
            return View(products);
        }
        [SetupUserClaimsActionFilter]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductForManipulateDto productModel)
        {
            MultipartFormDataContent formContent = CreateFormData(productModel);
            var request = new HttpRequestMessage(HttpMethod.Post, $"products");
            request.Content = formContent;

            string token = "";
            Request.Cookies.TryGetValue("token", out token);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        [SetupUserClaimsActionFilter]
        [HttpGet]
        public IActionResult Update(ProductModel productModelDto)
        {
            var productModel = new ProductUpdateViewModel()
            {
                Id = productModelDto.Id,
                ProductForManipulateDto = _mapper.Map<ProductForManipulateDto>(productModelDto)
            };
            return View(productModel);
        }
        [SetupUserClaimsActionFilter]
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel productModel)
        {
            MultipartFormDataContent formContent = CreateFormData(productModel.ProductForManipulateDto);
            var request = new HttpRequestMessage(HttpMethod.Put, $"products/{productModel.Id}");
            request.Content = formContent;

            string token = "";
            Request.Cookies.TryGetValue("token", out token);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        private static MultipartFormDataContent CreateFormData(ProductForManipulateDto productModel)
        {
            var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(productModel.Name), "Name");
            formContent.Add(new StringContent(productModel.DefaultQuantity.ToString()), "DefaultQuantity");

            if (productModel.Image != null)
            {
                formContent.Add(new StreamContent(productModel.Image.OpenReadStream()), "Image", $"{productModel.Image.FileName}");
            }

            return formContent;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid modelId)
        {
            var content = await SendHttpRequest(HttpMethod.Delete, $"products/{modelId}", null);
            return Ok();
        }
    }
}
