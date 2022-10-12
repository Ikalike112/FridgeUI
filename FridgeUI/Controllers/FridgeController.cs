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
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    public class FridgeController : BaseController
    {
        public IMapper _mapper { get; set; }
        public FridgeController(HttpClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var content = await SendHttpRequest(HttpMethod.Get, "fridge", null);
            var fridges = JsonConvert.DeserializeObject<List<FridgeModel>>(content);
            return View(fridges);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var contentProducts = SendHttpRequest(HttpMethod.Get, "products", null);
            var contentFridgeModels = SendHttpRequest(HttpMethod.Get, "fridgemodel", null);
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(await contentProducts);
            var fridgeModels = JsonConvert.DeserializeObject<List<FrdigeModelDto>>(await contentFridgeModels);
            var model = new FridgeModelsProductsViewModel()
            {
                FridgeModelsForCreateFridge = fridgeModels,
                ProductsForCreateFridge = _mapper.Map<List<ProductToCreateWithFridgeViewModel>>(products),
                FridgeForCreateModel = new FridgeForCreateDto()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FridgeModelsProductsViewModel model)
        {
            model.FridgeForCreateModel.FridgeModelId = Guid.Parse(model.ModelId);
            var selectedProducts = model.ProductsForCreateFridge.Where(x => x.Selected).ToList();
            model.FridgeForCreateModel.FridgeProducts = _mapper.Map<List<FridgeProductToCreateFromFridgeDto>>(selectedProducts);
            var content = await SendHttpRequest(HttpMethod.Post, "fridge", model.FridgeForCreateModel);
            var id = JsonConvert.DeserializeObject<string>(content);
            Console.WriteLine(id);
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await SendHttpRequest(HttpMethod.Delete, $"fridge/{id}",null);
            string url = this.Url.Action("Index", "Fridge");
            
            return Json(url);
        }
    }
}
