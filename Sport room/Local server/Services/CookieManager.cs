using System.Net;

namespace Local_server.Services
{
    internal static class CookieManager
    {
        public static string GetAuthenticatedCookie(CookieCollection cookies)
        {
            var cookie = cookies.FirstOrDefault(c => c.Name == "SessionId");

            return cookie is not null
                ? cookie.Value
                : string.Empty;
        }
    }
}
