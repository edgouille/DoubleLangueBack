namespace DoubleLangue.Domain;

public class User
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Role { get; init; }
    public DateTime CreatedAt { get; init; }
}