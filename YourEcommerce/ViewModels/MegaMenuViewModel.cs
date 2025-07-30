namespace YourEcommerce.ViewModels;

public class MegaMenuViewModel
{
    public Dictionary<Gender, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}