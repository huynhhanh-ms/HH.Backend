﻿namespace HH.Domain.Infrastructure.Auth
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string passwordHash, string inputPassword);
    }
}