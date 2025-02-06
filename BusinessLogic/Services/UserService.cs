using BusinessLogic.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Repositories;
using Data.Entities;

namespace BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterUserAsync(CreateUser createUser, CancellationToken cancellationToken)
    {
        bool isPasswordValid = ValidatePassword(createUser.Password);
        if (!isPasswordValid)
        {
            throw new RegistrationException("Password is invalid");
        }

        byte[] salt = PasswordManager.PasswordManager.GenerateSalt();
        byte[] password = PasswordManager.PasswordManager.HashPassword(createUser.Password, salt);
        
        User userToCreate = new User
        {
            Name = createUser.Name,
            Password = password,
            Salt = salt,
            Todos = new List<Todo>()
        };
        
        await _userRepository.CreateAsync(userToCreate, cancellationToken);
        
    }

    public async Task AuthenticateUserAsync(CreateUser createUser, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByNameAsync(createUser.Name, cancellationToken);

        bool success = PasswordManager.PasswordManager.VerifyPassword(createUser.Password, user.Password, user.Salt);

        if (!success)
        {
            throw new ApplicationException("Invalid password for user");
        }
    }
    
    private static bool ValidatePassword(string password)
    {
        bool isLengthValid = password.Length >= 8;
        bool containsLowerCase = password.Any(char.IsLower);
        bool containsUpperCase = password.Any(char.IsUpper);
        bool containsNumber = password.Any(char.IsNumber);
        return isLengthValid && containsLowerCase && containsUpperCase && containsNumber;
    }
}