namespace CareerMatch.UserServices.Controllers;

using CareerMatch.UserServices.Models;
using CareerMatch.UserServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _userService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userService.CreateUser(user);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] User user)
    {
        user.Id = id;
        _userService.UpdateUser(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        _userService.DeleteUser(id);
        return Ok();
    }
}
