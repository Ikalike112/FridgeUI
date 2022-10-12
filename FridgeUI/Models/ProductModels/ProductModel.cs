using System.ComponentModel.DataAnnotations;
using System;

namespace FridgeUI.Models.ProductModels
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Product name can't be null")]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity can't be lower than 0")]
        public int DefaultQuantity { get; set; }
        public string ImageSource { get; set; }
    }
}
