namespace YourEcommerce.ViewModels;

public class MenuDrawerViewModel
{
    public Dictionary<GenderViewModel, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}
