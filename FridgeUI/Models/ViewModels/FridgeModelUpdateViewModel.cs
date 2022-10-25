using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using System;
using System.Collections.Generic;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeModelUpdateViewModel
    {
        public FridgeModelForManipulateDto FridgeModelForManipulateDto { get; set; }
        public Guid Id { get; set; }
    }
}
