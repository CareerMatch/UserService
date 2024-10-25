namespace CareerMatch.UserServices.Services;

using CareerMatch.UserServices.Models;
using System;
using System.Collections.Generic;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserById(Guid id)
    {
        return _userRepository.GetUserById(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public void CreateUser(User user)
    {
        // Ensure CreatedAt is UTC
        user.CreatedAt = DateTime.UtcNow;

        // Set LastLogin to UTC (if applicable)
        user.LastLogin = DateTime.UtcNow;  // Optional if LastLogin is being set on creation
    
        // Convert DateOfBirth to UTC or ensure it is correctly handled
        user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);

        // Hash the password (assumed this is already working)
        user.PasswordHash = HashPassword(user.PasswordHash);

        _userRepository.AddUser(user);
    }


    public void UpdateUser(User user)
    {
        _userRepository.UpdateUser(user);
    }

    public void DeleteUser(Guid id)
    {
        _userRepository.DeleteUser(id);
    }

    private string HashPassword(string password)
    {
        // Implement password hashing logic (e.g., using BCrypt or another method)
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
