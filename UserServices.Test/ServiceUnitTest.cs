
using CareerMatch.UserServices.Models;
using CareerMatch.UserServices.Repositories;
using CareerMatch.UserServices.Services;

using Moq;
using Xunit;

namespace UserService.Test;

public class ServiceUnitTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserReadService _userReadService;
    private readonly UserWriteService _userWriteService;

    public ServiceUnitTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userReadService = new UserReadService(_userRepositoryMock.Object);
        _userWriteService = new UserWriteService(_userRepositoryMock.Object);
    }

    [Fact]
    public void GetUserById_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockUser = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" };

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(mockUser);

        // Act
        var result = _userReadService.GetUserById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public void GetAllUsers_ReturnsUserList()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
            new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com" }
        };

        _userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(users);

        // Act
        var result = _userReadService.GetAllUsers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void CreateUser_SavesUser()
    {
        // Arrange
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PasswordHash = "plaintextpassword"
        };

        _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()));

        // Act
        _userWriteService.CreateUser(user);

        // Assert
        _userRepositoryMock.Verify(repo => repo.AddUser(It.Is<User>(u => u.Email == "johndoe@example.com")),
            Times.Once);
        Assert.Equal(DateTime.UtcNow.Date, user.CreatedAt.Date);
        Assert.Equal(DateTime.UtcNow.Date, user.LastLogin.Date);
    }

    [Fact]
    public void UpdateUser_CallsRepositoryUpdate()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
        _userRepositoryMock.Setup(repo => repo.UpdateUser(user));

        // Act
        _userWriteService.UpdateUser(user);

        // Assert
        _userRepositoryMock.Verify(repo => repo.UpdateUser(user), Times.Once);
    }

    [Fact]
    public void DeleteUser_RemovesUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.DeleteUser(userId));

        // Act
        _userWriteService.DeleteUser(userId);

        // Assert
        _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
    }
}