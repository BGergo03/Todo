using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Controller]
[Route("[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;
   private readonly IJwtService _jwtService;
   
   public UserController(IUserService userService, IJwtService jwtService)
   {
      _userService = userService;
      _jwtService = jwtService;
   }

   [HttpPost]
   [Route("register")]
   public async Task<ActionResult> RegisterUserAsync([FromBody]CreateUser createUser, CancellationToken cancellationToken)
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
   public async Task<ActionResult> LoginUserAsync([FromBody]CreateUser createUser, CancellationToken cancellationToken)
   {
      try
      {
         await _userService.AuthenticateUserAsync(createUser, cancellationToken);
      }
      catch (Exception exception)
      {
         return BadRequest(exception.Message);
      }

      var token = _jwtService.GenerateJwtToken(createUser.Name);
      return Ok(new { token });
   }
}