using System;

namespace FridgeUI.Models.FridgeModels
{
    public class FridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string FridgeModelName { get; set; }
    }
}
