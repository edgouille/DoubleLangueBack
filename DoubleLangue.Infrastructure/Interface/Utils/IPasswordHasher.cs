namespace DoubleLangue.Infrastructure.Interface.Utils;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}

