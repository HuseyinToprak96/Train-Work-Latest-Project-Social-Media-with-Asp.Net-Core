using Microsoft.AspNetCore.Http;

namespace SocialMedia.Web.Helpers
{
    public class SessionHelper
    {
        public static string GetSession(HttpContext context,string key)
        {
            return context.Session.GetString(key);
        }
        public static void SetSession(HttpContext context,string key,string value)
        {
            var control=context.Session.GetString(key);
            if (control != null) return;
            context.Session.SetString(key, value);
        }
    }
}
