using System.Collections;

namespace YourEcommerce.Helpers
{
    public static class CollectionExtensions
    {
        public static string ToNamesString(this IEnumerable? collection)
        {
            if (collection == null) return "-";

            var items = collection.Cast<object>()
                                  .Select(x => x?.ToString() ?? "-")
                                  .ToList();

            return items.Any() ? string.Join(", ", items) : "-";
        }
    }
}