using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.ViewModels;

public class MegaMenuViewModel
{
    public Dictionary<GenderDto, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}