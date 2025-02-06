using BusinessLogic.DTOs;

namespace BusinessLogic.Services;

public interface IUserService
{
    public Task RegisterUserAsync(CreateUser createUser, CancellationToken cancellationToken);

    public Task AuthenticateUserAsync(CreateUser createUser, CancellationToken cancellationToken);
}