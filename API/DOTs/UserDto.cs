namespace API;

public class UserDTO
{
    public required string userName { get; set; }
    public required string Token { get; set; }

    public required string KnownAs { get; set; }

    public required string Gender { get; set; }
    public string? PhotoUrl { get; set; }

}