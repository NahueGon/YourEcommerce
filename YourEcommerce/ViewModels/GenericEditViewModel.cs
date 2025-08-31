using System.Reflection;

namespace YourEcommerce.ViewModels
{
    public class GenericEditViewModel<T>
    {
        public T? Item { get; set; }
        public string EntityName { get; set; } = default!;
        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();

        public void OrderProperties()
        {
            if (Item == null) return;

            var props = Item.GetType().GetProperties().ToList();

            PropertyInfo? imageProp = null;
            foreach (var prop in props)
            {
                if (prop.Name.Contains("Image"))
                {
                    imageProp = prop;
                    break;
                }
            }

            if (imageProp != null)
            {
                props.Remove(imageProp);
                props.Insert(0, imageProp);
            }

            Properties = props;
        }
    }
}