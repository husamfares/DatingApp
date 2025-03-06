namespace API;

public class UserDTO
{
    public required string userName { get; set; }
    public required string Token { get; set; }

    public string? PhotoUrl { get; set; }

}