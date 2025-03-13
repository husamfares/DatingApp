using System.Security.Claims;

namespace API.Extenstions;

public static class ClaimsPrincipleExtentions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.Name)
        ?? throw new Exception("Cannot get the username from token");

        return username;
    }

    public static int GetUserid(this ClaimsPrincipal user)
    {
        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new Exception("Cannot get the userId from token"));

        return userId;
    }
}