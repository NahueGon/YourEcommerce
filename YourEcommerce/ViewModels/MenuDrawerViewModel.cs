namespace YourEcommerce.ViewModels;

public class MenuDrawerViewModel
{
    public Dictionary<Gender, Dictionary<string, List<string>>> MenuStructure { get; set; } = new();
}
