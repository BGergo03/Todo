using BusinessLogic.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Extensions;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    
    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        ulong id =  (ulong) await _dbContext.Users.CountAsync(cancellationToken) + 1;
        var userToCreate = new User
        {
            Id = id,
            Name = user.Name,
            Password = user.Password,
            Salt = user.Salt,
            Todos = new List<Todo>()
        };

        await _dbContext.AddAsync(userToCreate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> GetUserByNameAsync(string name, CancellationToken cancellationToken)
    {
        User? result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);

        if (result == null)
        {
            throw new UserNotFoundException($"User was not found: {name}");
        }

        return result;
    }
}