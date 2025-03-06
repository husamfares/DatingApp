using System.Security.Claims;

namespace API.Extenstions;

public static class ClaimsPrincipleExtentions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if(username == null) throw new Exception("Cannot get the username from token");

        return username;
    }
}