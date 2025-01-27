using BusinessLogic.DTOs;

namespace BusinessLogic.Repositories;

public interface ITodoRepository
{
    public Task<IList<TodoDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<TodoDto> GetByIdAsync(ulong id, CancellationToken cancellationToken);

    public Task DeleteByIdAsync(ulong id, CancellationToken cancellationToken);

    public Task CreateAsync(TodoDto todoDto, CancellationToken cancellationToken);
}