using CareerMatch.UserServices.Repositories;
using CareerMatch.UserServices.Models;

namespace CareerMatch.UserServices.Services;

public class UserWriteService : IUserWriteService
{
    private readonly IUserRepository _userRepository;

    public UserWriteService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void CreateUser(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        
        user.LastLogin = DateTime.UtcNow;  
        
        user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);
        
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
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}