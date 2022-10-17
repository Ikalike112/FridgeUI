using FridgeUI.Models.FridgeModelModels;
using FridgeUI.Models.FridgeModels;
using FridgeUI.Models.FridgeProductsModels;
using FridgeUI.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeProductUpdateViewModel : FridgeProductManipulateViewModel
    { 
        public Guid FridgeProductId { get; set; }
        
    }
}
