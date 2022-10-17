using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using System.Collections.Generic;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeForUpdateViewModel
    {
        public FridgeForUpdateDto FridgeForUpdateDto { get; set; }
        public List<FrdigeModelDto> FridgeModels { get; set; }
    }
}
