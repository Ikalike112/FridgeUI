using FridgeUI.Models.FridgeProductsModels;
using System.Collections.Generic;

namespace FridgeUI.Models.FridgeModels
{
    public class FridgeForCreateDto : FridgeForManipulateDto
    {
        public List<FridgeProductToCreateFromFridgeDto>? FridgeProducts { get; set; }

    }
}
