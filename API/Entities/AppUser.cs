using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    
    //[Key] // the default id is primary key for db but in case i want another id use this
    public int Id { get; set; }
    public required String Name { get; set; }
    
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

}
