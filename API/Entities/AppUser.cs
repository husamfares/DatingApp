using System.ComponentModel.DataAnnotations;
using API.Extenstions;

namespace API.Entities;

public class AppUser
{

    //[Key] // the default id is primary key for db but in case i want another id use this
    public int Id { get; set; }
    public required string Name { get; set; }

    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];

    public DateOnly DataOfBirth { get; set; }

    public required string KnownAs { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public required string Gender { get; set; }

    public string? Introduction { get; set; }

    public string? Intrests { get; set; }

    public string? LookingFor { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

    public List<Photo> Photos { get; set; } = [];

    public List<UserLike> LikedByUsers { get; set; } = [];

    public List<UserLike> LikedUsers { get; set; }=[];

}
