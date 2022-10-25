using System.ComponentModel.DataAnnotations;
using System;

namespace FridgeUI.Models.FridgeProductsModels
{
    public class FridgeProductToCreateFromFridgeDto
    {
        //[Required]
        public Guid ProductId { get; set; }
       // [Range(0, int.MaxValue, ErrorMessage = "Quantity can't be lower than 0")]
        public int? Quantity { get; set; }
    }
}
