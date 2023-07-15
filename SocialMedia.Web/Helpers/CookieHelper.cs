using Microsoft.AspNetCore.Http;
using System;

namespace SocialMedia.Web.Helpers
{
    public class CookieHelper
    {
        public static string GetCookieValue(HttpContext httpContext, string key)
        {
            if (httpContext.Request.Cookies.TryGetValue(key, out string value))
            {
                return value;
            }
            return null;
        }

        public static void SetCookieValue(HttpContext httpContext, string key, string value, int? expirationDays = null)
        {
            CookieOptions options = new CookieOptions
            {
                IsEssential = true
            };
            if (expirationDays.HasValue)
            {
                options.Expires = DateTime.Now.AddDays(expirationDays.Value);
            }
            httpContext.Response.Cookies.Append(key, value, options);
        }
    }
}
