using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDTO
{
    [Required]
    public String Username { get; set; }
    [Required]
    public String Password { get; set; }
}
