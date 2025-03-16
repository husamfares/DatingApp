using API.Helpers;

namespace API.Helpers;

public class LikesParams : PaginaitionParams
{
    public int UserId { get; set; }

    public required string Predicate { get; set; } = "liked";
}