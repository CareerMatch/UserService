using CareerMatch.UserServices.Data;
using CareerMatch.UserServices.Models;

namespace CareerMatch.UserServices.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public User GetUserById(Guid id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id && !u.IsDeleted);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }
        return user;
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
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        user.IsDeleted = true; // Perform soft delete
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
