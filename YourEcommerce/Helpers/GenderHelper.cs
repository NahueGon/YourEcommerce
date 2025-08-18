using YourEcommerce.ViewModels;

namespace YourEcommerce.Helpers;

public static class GenderHelper
{
    public static string GetGenderName(Gender gender) => gender switch
    {
        Gender.Unisex => "Unisex",
        Gender.Male => "Hombres",
        Gender.Female => "Mujeres",
        Gender.Kids => "Niños",
        _ => "Desconocido"
    };
}