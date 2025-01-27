using BusinessLogic.DTOs;
using Data.Entities;

namespace BusinessLogic.Extensions;

public static class TodoExtensions
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto
        {
            Title = todo.Title,
            Description = todo.Description
        };
    }
}