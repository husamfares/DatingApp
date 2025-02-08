using System.ComponentModel.DataAnnotations;

namespace API.DOTs;
// this class for transfer data as object not like parameter
public class RegisterDto
{
    [Required]//what does mean
    public required string userName { get; set; }
    public required string password { get; set; }
}