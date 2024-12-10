namespace CareerMatch.UserServices.Controllers;

using CareerMatch.UserServices.Models;
using CareerMatch.UserServices.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly IUserWriteService _userWriteService;

    public UserManagementController(IUserWriteService userWriteService)
    {
        _userWriteService = userWriteService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userWriteService.CreateUser(user);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] User user)
    {
        user.Id = id;  // Ensure the ID from the route matches the user object
        _userWriteService.UpdateUser(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        _userWriteService.DeleteUser(id);
        return Ok();
    }
}