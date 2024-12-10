namespace CareerMatch.UserServices.Controllers;

using CareerMatch.UserServices.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserReadController : ControllerBase
{
    private readonly IUserReadService _userReadService;

    public UserReadController(IUserReadService userReadService)
    {
        _userReadService = userReadService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _userReadService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userReadService.GetAllUsers();
        return Ok(users);
    }
}