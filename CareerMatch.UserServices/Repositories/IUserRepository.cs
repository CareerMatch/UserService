using CareerMatch.UserServices.Models;
using System;
using System.Collections.Generic;

namespace CareerMatch.UserServices.Repositories;


public interface IUserRepository
{
    User GetUserById(Guid id);
    IEnumerable<User> GetAllUsers();
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(Guid id); // Soft delete
}