using System.Reflection;

namespace YourEcommerce.ViewModels;

public class TableDashboardViewModel<T>
{
    public string? Title { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public string? ApiBaseUrl { get; set; } 

    public List<PropertyInfo> GetOrderedProperties()
    {
        var firstItem = Items.FirstOrDefault();
        if (firstItem == null) return new List<PropertyInfo>();

        var properties = firstItem.GetType().GetProperties().ToList();

        var imageProp = properties.FirstOrDefault(p => p.Name.Contains("Image"));
        if (imageProp != null)
        {
            properties.Remove(imageProp);
            properties.Insert(1, imageProp);
        }

        return properties;
    }
}