using System.ComponentModel.DataAnnotations;

namespace API.DOTs;
// this class for transfer data as object not like parameter
public class RegisterDto
{
    [Required]//what does mean
    public  string userName { get; set; } = string.Empty;
    [Required]
    [StringLength(8, MinimumLength =4)]
    public  string password { get; set; } = string.Empty;
}