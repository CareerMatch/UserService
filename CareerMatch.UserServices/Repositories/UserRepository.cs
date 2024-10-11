namespace CareerMatch.UserServices.Repositories;

using CareerMatch.UserServices.Data;
using CareerMatch.UserServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public User GetUserById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.Where(u => !u.IsDeleted).ToList();
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(Guid id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            user.IsDeleted = true; // Perform soft delete
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
