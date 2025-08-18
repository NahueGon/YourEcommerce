namespace YourEcommerce.ViewModels;

public class MegaMenuViewModel
{
    public Dictionary<GenderViewModel, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}