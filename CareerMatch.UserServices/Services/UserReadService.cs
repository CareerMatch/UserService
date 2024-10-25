using CareerMatch.UserServices.Models;

namespace CareerMatch.UserServices.Services;

public class UserReadService : IUserReadService
{
    private readonly IUserRepository _userRepository;

    public UserReadService(IUserRepository userRepository)
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
}