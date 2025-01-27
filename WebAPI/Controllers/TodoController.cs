using BusinessLogic.DTOs;
using BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Controller]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    [HttpGet]
    public async Task<IList<TodoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _todoRepository.GetAllAsync(cancellationToken);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<TodoDto>> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        try
        {
            return await _todoRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception exception)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        try
        {
            await _todoRepository.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] TodoDto todoDto, CancellationToken cancellationToken)
    {
        try
        {
            await _todoRepository.CreateAsync(todoDto, cancellationToken);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }
}