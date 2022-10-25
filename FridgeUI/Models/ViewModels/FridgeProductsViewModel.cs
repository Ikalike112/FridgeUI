using FridgeUI.Models.FridgeProductsModels;
using System;
using System.Collections.Generic;

namespace FridgeUI.Models.ViewModels
{
    public class FridgeProductsViewModel
    {
        public List<FridgeProductDto> FridgeProducts { get; set; }
        public Guid FridgeId { get; set; }
    }
}
