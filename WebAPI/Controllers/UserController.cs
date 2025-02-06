using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Controller]
[Route("[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;
   
   public UserController(IUserService userService)
   {
      _userService = userService;
   }

   [HttpPost]
   [Route("register")]
   public async Task<ActionResult> RegisterUserAsync(CreateUser createUser, CancellationToken cancellationToken)
   {
      try
      {
         await _userService.RegisterUserAsync(createUser, cancellationToken);
      }
      catch (Exception exception)
      {
         return BadRequest(exception.Message);
      }
      
      return Ok();
   }
   
   [HttpPost]
   [Route("login")]
   public async Task<ActionResult> LoginUserAsync(CreateUser createUser, CancellationToken cancellationToken)
   {
      try
      {
         await _userService.AuthenticateUserAsync(createUser, cancellationToken);
      }
      catch (Exception exception)
      {
         return BadRequest(exception.Message);
      }
      
      return Ok();
   }
}