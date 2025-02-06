using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.PasswordManager;

public class PasswordManager
{
    private const int SaltLength = 64;
    private const int Iterations = 350000;
    private const int OutputLength = 64;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

    public static byte[] HashPassword(string password, byte[] salt)
    {
        var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName,
            OutputLength);

        return hashedPassword;
    }
    public static byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(SaltLength);

    public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName, OutputLength);
        
        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}