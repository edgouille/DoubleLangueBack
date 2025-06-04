using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto;

public class UserLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
