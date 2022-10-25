using FridgeUI.Models.ProductModels;
using System;

namespace FridgeUI.Models.FridgeProductsModels
{
    public class FridgeProductDto
    {
        public Guid Id { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
