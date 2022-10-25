using AutoMapper;
using FridgeUI.ActionFilters;
using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using FridgeUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FridgeUI.Controllers
{
    [Route("[controller]/[action]")]
    public class FridgeModelController : BaseController
    {
        public IMapper _mapper { get; set; }
        public FridgeModelController(HttpClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var content = await SendHttpRequest(HttpMethod.Get, "fridgemodel", null);
            var fridgesmodels = JsonConvert.DeserializeObject<List<FrdigeModelDto>>(content);
            return View(fridgesmodels);
        }
        [SetupUserClaimsActionFilter]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FridgeModelForManipulateDto fridgeModel)
        {
            var content = await SendHttpRequest(HttpMethod.Post, "fridgemodel", fridgeModel);
            return RedirectToAction("Index");
        }
        [SetupUserClaimsActionFilter]
        [HttpGet]
        public IActionResult Update(FrdigeModelDto fridgeModelDto)
        {

            var fridgeModel = new FridgeModelUpdateViewModel()
            {
                Id = fridgeModelDto.Id,
                FridgeModelForManipulateDto = _mapper.Map<FridgeModelForManipulateDto>(fridgeModelDto)
            };
            return View(fridgeModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(FridgeModelUpdateViewModel fridgeModel)
        {
            var content = await SendHttpRequest(HttpMethod.Put, $"fridgemodel/{fridgeModel.Id}", fridgeModel.FridgeModelForManipulateDto);
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid modelId)
        {
            var content = await SendHttpRequest(HttpMethod.Delete, $"fridgemodel/{modelId}", null);
            return Ok();
        }
    }
}
