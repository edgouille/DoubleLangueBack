using System;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Domain.Dto.User;

public class UserResponseDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserRoleEnum Role { get; set; }
    public DateTime LastActivity { get; set; }
}
