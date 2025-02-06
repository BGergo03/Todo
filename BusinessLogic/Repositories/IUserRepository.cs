using Data.Entities;

namespace BusinessLogic.Repositories;

public interface IUserRepository
{
    public Task CreateAsync(User user, CancellationToken cancellationToken);

    public Task<User> GetUserByNameAsync(string name, CancellationToken cancellationToken);
}