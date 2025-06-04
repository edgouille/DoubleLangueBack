using System.ComponentModel.DataAnnotations;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Domain.Dto;

public class UserUpdateDto
{
    public string? UserName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Password { get; set; }

    public UserRoleEnum? Role { get; set; }
}
