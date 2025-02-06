using BusinessLogic.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Extensions;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    
    public async Task<IList<TodoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Todos.Select(t => t.ToDto()).ToListAsync(cancellationToken);
    }

    public async Task<TodoDto> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        return (await GetModelByIdAsync(id, cancellationToken)).ToDto();
    }

    public async Task DeleteByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        Todo todoToDelete = await GetModelByIdAsync(id, cancellationToken);
        _dbContext.Todos.Remove(todoToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(TodoDto todoDto, CancellationToken cancellationToken)
    {
        ulong id = (ulong)await _dbContext.Todos.CountAsync(cancellationToken) + 1;
        var todoToCreate = new Todo
        {
            Id = id,
            Title = todoDto.Title,
            Description = todoDto.Description
        };
        
        await _dbContext.Todos.AddAsync(todoToCreate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Todo> GetModelByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        Todo? result = await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (result is null)
        {
            throw new TodoNotFoundException($"Todo was not found: {id}");
        }

        return result;
    }
}