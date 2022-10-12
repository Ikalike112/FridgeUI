using System.ComponentModel.DataAnnotations;

namespace FridgeUI.Models.ProductModels
{
    public class ProductToCreateWithFridgeViewModel : ProductModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Quantity can't be lower than 0")]
        public int Quantity { get; set; }
        public bool Selected { get; set; }
    }
}
