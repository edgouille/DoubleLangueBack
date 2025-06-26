using System.ComponentModel.DataAnnotations;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Domain.Dto.User;

public class UserCreateDto
{
    [Required]
    public string UserName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public UserRoleEnum Role { get; set; }
}
