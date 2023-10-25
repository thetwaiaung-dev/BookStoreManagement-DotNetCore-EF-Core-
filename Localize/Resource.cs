using Microsoft.Extensions.Localization;

namespace BookManagement.Localize
{
    public class Resource
    {
        
    }

   public static class ResourceExtension
    {
        public static IStringLocalizer<Resource> _localizer { get; set; }
       
        public static void Configure(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }

        public static string GetResource(this string str)
        {
            return _localizer?[str];
        }
    }
}
