namespace backend.Security;

public class CookieHelper
{
    public static void SetAccessToken(HttpResponse response, string token)
    {
        response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });
    }

    public static void SetRefreshToken(HttpResponse response, string token)
    {
        response.Cookies.Append("refresh_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(30)
        });
    }

    public static void ClearTokens(HttpResponse response)
    {
        response.Cookies.Delete("access_token");
        response.Cookies.Delete("refresh_token");
    }
}