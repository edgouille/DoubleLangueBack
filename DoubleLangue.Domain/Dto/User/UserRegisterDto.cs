using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.User;

public class UserRegisterDto
{
    [Required]
    public string UserName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
