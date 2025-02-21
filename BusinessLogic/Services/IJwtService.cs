namespace BusinessLogic.Services;

public interface IJwtService
{
    public string GenerateJwtToken(string username);
}