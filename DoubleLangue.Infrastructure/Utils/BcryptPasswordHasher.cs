﻿using DoubleLangue.Infrastructure.Interface.Utils;

namespace DoubleLangue.Infrastructure.Utils;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

