using AutoMapper;
using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using FridgeUI.Models.FridgeProductsModels;
using FridgeUI.Models.ProductModels;
using FridgeUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    [Route("[controller]/[action]")]
    public class FridgeProductsController : BaseController
    {
        public IMapper _mapper { get; set; }
        public FridgeProductsController(HttpClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }
        [HttpGet("{fridgeId}")]
        public async Task<IActionResult> Products(Guid fridgeId)
        {
            var content = await SendHttpRequest(HttpMethod.Get, $"fridgeproducts/{fridgeId}", null);
            var fridgesProducts = JsonConvert.DeserializeObject<List<FridgeProductDto>>(content);
            var productsViewModel = new FridgeProductsViewModel()
            {
                FridgeId = fridgeId,
                FridgeProducts = fridgesProducts
            };
            return View(productsViewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create(Guid fridgeId)
        {
            var contentProducts = SendHttpRequest(HttpMethod.Get, "products", null);
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(await contentProducts);
            var content = await SendHttpRequest(HttpMethod.Get, $"fridge", null);
            var fridges = JsonConvert.DeserializeObject<List<FridgeDto>>(content);
            var model = new FridgeProductManipulateViewModel()
            {
                FridgeProductForManipulateDto = new FridgeProductForManipulateDto()
                {
                    FridgeId = fridgeId
                },
                Fridges = fridges,
                Products = products
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FridgeProductManipulateViewModel model)
        {
            var contentProducts = await SendHttpRequest(HttpMethod.Post, $"fridgeproducts", model.FridgeProductForManipulateDto);

            return RedirectToAction($"Products", new { fridgeId = model.FridgeProductForManipulateDto.FridgeId });
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var contentProducts = SendHttpRequest(HttpMethod.Get, "products", null);
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(await contentProducts);
            var content = await SendHttpRequest(HttpMethod.Get, $"fridge", null);
            var fridges = JsonConvert.DeserializeObject<List<FridgeDto>>(content);
            var contentFridgeProduct = await SendHttpRequest(HttpMethod.Get, $"fridgeproducts/GetByFridgeProductId/{id}", null);
            var fridgeProduct = JsonConvert.DeserializeObject<FridgeProductForManipulateDto>(contentFridgeProduct);
            var model = new FridgeProductUpdateViewModel()
            {
                FridgeProductId = id,
                FridgeProductForManipulateDto = fridgeProduct,
                Fridges = fridges,
                Products = products
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(FridgeProductUpdateViewModel fridgeModel)
        {
            var contentProducts = await SendHttpRequest(HttpMethod.Put, $"fridgeproducts/{fridgeModel.FridgeProductId}", fridgeModel.FridgeProductForManipulateDto);
            return RedirectToAction($"Products",new { fridgeId = fridgeModel.FridgeProductForManipulateDto.FridgeId});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var content = await SendHttpRequest(HttpMethod.Delete, $"fridgeproducts/{productId}", null);
            string url = this.Url.Action("Products", "FridgeProducts");

            return Json(url);
        }
    }
}
