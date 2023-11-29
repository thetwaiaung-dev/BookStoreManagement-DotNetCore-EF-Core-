using Microsoft.AspNetCore.Components.Web;
using Serilog;

namespace BookManagement.Helper
{
    public class LogHelper
    {
        //public LogHelper()
        //{
        //    Log.Logger = new LoggerConfiguration()
        //                     .MinimumLevel.Information()
        //                     .WriteTo.File("logs/bookLog.txt", rollingInterval: RollingInterval.Hour)
        //                     .CreateLogger();
        //}

        public void Info(string text)
        {
            Log.Information(text);
        }

        public static void Debug(string text)
        {
            Log.Debug(text);
        }

        public static void Error(string text)
        {
            Log.Error(text);
        }
    }
}
