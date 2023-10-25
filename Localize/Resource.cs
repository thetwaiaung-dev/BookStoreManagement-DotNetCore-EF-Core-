using Microsoft.Extensions.Localization;

namespace BookManagement.Localize
{
    public class Resource
    {
    }

   public static class ResourceExtension
    {
        public static IStringLocalizer<Resource> Localizer { get; set; }
       
        public static void Configure(IStringLocalizer<Resource> localizer)
        {
            Localizer = localizer;
        }

        public static string GetResource(this string str)
        {
            return Localizer?[str];
        }
    }
}
