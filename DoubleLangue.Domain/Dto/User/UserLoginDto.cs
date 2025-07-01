using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.User;

public class UserLoginDto
{
    [Required]
    public string Identifier { get; set; } = string.Empty; // username or email

    [Required]
    public string Password { get; set; } = string.Empty;
}
