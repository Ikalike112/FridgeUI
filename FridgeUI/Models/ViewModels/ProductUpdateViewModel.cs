using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.ProductModels;
using System;

namespace FridgeUI.Models.ViewModels
{
    public class ProductUpdateViewModel
    {
        public ProductForManipulateDto ProductForManipulateDto { get; set; }
        public Guid Id { get; set; }
    }
}
