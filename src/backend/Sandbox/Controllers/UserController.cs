using Microsoft.AspNetCore.Mvc;
using Sandbox.App.Contracts;
using Sandbox.App.Interfaces.Services;

namespace Sandbox.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersAsync(cancellationToken);
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        await _userService.Register(request);

        return Ok();
    }
}
