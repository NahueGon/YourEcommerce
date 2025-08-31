namespace YourEcommerce.Helpers;

public static class StringHelpers
{
    public static string TranslateToSpanish(this string? title)
    {
        return title switch
        {
            "products" => "Productos",
            "users" => "Usuarios",
            "categories" => "Categorías",
            "genders" => "Géneros",
            "brands" => "Marcas",
            "sports" => "Deportes",
            null => string.Empty,
            _ => title
        };
    }

    public static string ToSingular(this string? title)
    {
        return title switch
        {
            "Productos" => "Producto",
            "Usuarios" => "Usuario",
            "Categorías" => "Categoría",
            "Géneros" => "Género",
            "Marcas" => "Marca",
            "Deportes" => "Deporte",
            null => string.Empty,
            _ => title
        };
    }
}