using CareerMatch.UserServices.Models;

namespace CareerMatch.UserServices.Services;

public interface IUserWriteService
{
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(Guid id);
}