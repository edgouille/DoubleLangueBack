using System.ComponentModel.DataAnnotations;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Domain.Models;

public class User
{
    [Key]
    public Guid Id { get; init; }
    [Required]
    public string UserName { get; init; }
    [Required, EmailAddress]
    public string Email { get; init; }
    [Required]
    public string Password { get; init; }
    [Required]
    public UserRoleEnum Role { get; init; }
    [Required]
    public DateTime CreatedAt { get; init; }
}