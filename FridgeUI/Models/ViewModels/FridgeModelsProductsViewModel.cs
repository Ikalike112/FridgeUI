using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using FridgeUI.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeModelsProductsViewModel
    {
        [Required(ErrorMessage = "Model is a required field.")]
        public string ModelId { get; set; }
        public FridgeForCreateDto FridgeForCreateModel { get; set; }
        public List<FrdigeModelDto> FridgeModelsForCreateFridge { get; set; }
        public List<ProductToCreateWithFridgeViewModel> ProductsForCreateFridge { get; set; }
    }
}
