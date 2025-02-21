namespace BusinessLogic.Services;

public interface IPasswordService
{
    public byte[] GenerateSalt();

    public byte[] GenerateHash(string password, byte[] salt);

    public bool VerifyPassword(string password, byte[] salt, byte[] hash);
}