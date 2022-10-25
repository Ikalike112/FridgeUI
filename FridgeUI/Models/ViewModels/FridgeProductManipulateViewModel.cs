using FridgeUI.Models.FridgeModels;
using FridgeUI.Models.FridgeProductsModels;
using FridgeUI.Models.ProductModels;
using System.Collections.Generic;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeProductManipulateViewModel
    {
        public FridgeProductForManipulateDto FridgeProductForManipulateDto { get; set; }
        public List<FridgeDto> Fridges { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
