using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.ViewModels;

public class MenuDrawerViewModel
{
    public Dictionary<GenderDto, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}
