using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services;

public class PasswordService : IPasswordService
{
    private const int SaltLength = 64;
    private const int Iterations = 350000;
    private const int OutputLength = 64;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

    public byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(SaltLength);

    public byte[] GenerateHash(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
            salt, 
            Iterations,
            HashAlgorithmName,
            OutputLength);
    }

    public bool VerifyPassword(string password, byte[] salt, byte[] hashedPassword)
    {
        byte[] hash = GenerateHash(password, salt);

        return CryptographicOperations.FixedTimeEquals(hash, hashedPassword);
    }
}