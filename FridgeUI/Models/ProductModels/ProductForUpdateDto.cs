using System;

namespace FridgeUI.Models.ProductModels
{
    public class ProductForUpdateDto : ProductForManipulateDto
    {
        public Guid Id { get; set; }
    }
}
