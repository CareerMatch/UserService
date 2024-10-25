namespace CareerMatch.UserServices.Services;

using CareerMatch.UserServices.Models;
using System;
using System.Collections.Generic;

public interface IUserService
{
    User GetUserById(Guid id);
    IEnumerable<User> GetAllUsers();
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(Guid id);
}
