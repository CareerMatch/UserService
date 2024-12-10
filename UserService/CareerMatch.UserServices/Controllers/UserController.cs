namespace CareerMatch.UserServices.Controllers;

using CareerMatch.UserServices.Models;
using CareerMatch.UserServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserReadService _userReadService;
    private readonly IUserWriteService _userWriteService;
    // Inject both the read and write services
    public UsersController(IUserReadService userReadService, IUserWriteService userWriteService)
    {
        _userReadService = userReadService;
        _userWriteService = userWriteService;
    }

    // Read operation to get a user by ID
    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _userReadService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // Read operation to get all users
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userReadService.GetAllUsers();
        return Ok(users);
    }
    
    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userWriteService.CreateUser(user);
        return Ok();
    }

    // Write operation to update an existing user
    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] User user)
    {
        user.Id = id; // Ensure that the ID from the route is used
        _userWriteService.UpdateUser(user);
        return Ok();
    }

    // Write operation to delete a user by ID
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        _userWriteService.DeleteUser(id);
        return Ok();
    }
}