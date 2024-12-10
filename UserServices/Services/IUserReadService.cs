using CareerMatch.UserServices.Models;

namespace CareerMatch.UserServices.Services;

public interface IUserReadService
{
    User GetUserById(Guid id);
    IEnumerable<User> GetAllUsers();
}
