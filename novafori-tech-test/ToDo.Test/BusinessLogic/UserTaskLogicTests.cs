using Moq;
using ToDo.BusinessLogic;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Test.BusinessLogic;

public class UserTaskLogicTests
{
    [Fact]
    public async Task GetTasksAsync_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };
        var userTasks = new List<UserTask> { userTask };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetAllAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(userTasks);


        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        var result = await userTaskLogic.GetTasksAsync(user.Id);

        // Assert
        Assert.Equal(result, userTasks);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
        mockedUserTaskRepository.Verify(x => x.GetAllAsync(It.Is<Guid>(x => x == userTask.Id)), Times.Once);
    }

    [Fact]
    public async Task GetTasksAsync_ShouldThrowUserNotFoundIfUserIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = userId
        };
        var userTasks = new List<UserTask> { userTask };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userId)));

        mockedUserTaskRepository
            .Setup(x => x.GetAllAsync(It.Is<Guid>(u => u == userId)))
            .ReturnsAsync(userTasks);

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserNotFoundException>(() => userTaskLogic.GetTasksAsync(userId));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == userId)), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userTask.Id)))
            .ReturnsAsync(userTask);


        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        var result = await userTaskLogic.GetTaskAsync(user.Id, userTask.Id);

        // Assert
        Assert.Equal(result, userTask);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_ShouldThrowUserNotFoundExceptionIfUserIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = userId
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userId)));

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userTask.Id)))
            .ReturnsAsync(userTask);

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserNotFoundException>(() => userTaskLogic.GetTaskAsync(userId, userTask.Id));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == userId)), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_ShouldThrowTaskNotFoundExceptionIfTaskIsNull()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<TaskNotFoundException>(() => userTaskLogic.GetTaskAsync(user.Id, Guid.NewGuid()));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task GetTaskAsync_ShouldThrowUserDoesNotOwnTaskException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = Guid.NewGuid()
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserDoesNotOwnTaskException>(() => userTaskLogic.CreateTaskAsync(user.Id, userTask));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task CreateTaskAsync_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        var result = await userTaskLogic.CreateTaskAsync(user.Id, userTask);

        // Assert
        Assert.Equal(result, userTask);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
        mockedUserTaskRepository
            .Verify(
                x => x.CreateAsync(
                    It.Is<Guid>(x => x == user.Id),
                    It.Is<UserTask>(y => y == userTask)
                ),
                Times.Once);
    }

    [Fact]
    public async Task CreateTaskAsync_ShouldThrowUserNotFoundExceptionIfUserIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = userId
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userId)));

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserNotFoundException>(() => userTaskLogic.CreateTaskAsync(userId, userTask));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == userId)), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        var result = await userTaskLogic.UpdateTaskAsync(user.Id, userTask.Id, userTask);

        // Assert
        Assert.Equal(result, userTask);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
        mockedUserTaskRepository.Verify(x => x.UpdateAsync(It.Is<UserTask>(x => x == userTask)), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldThrowUserNotFoundExceptionIfUserIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = userId
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userId)));

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserNotFoundException>(() =>
            userTaskLogic.UpdateTaskAsync(userId, userTask.Id, userTask));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == userId)), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldThrowTaskNotFoundExceptionIfTaskIsNull()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<TaskNotFoundException>(() =>
            userTaskLogic.UpdateTaskAsync(user.Id, Guid.NewGuid(), new UserTask()));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldThrowUserDoesNotOwnTaskException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = Guid.NewGuid()
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await Assert.ThrowsAsync<UserDoesNotOwnTaskException>(() =>
            userTaskLogic.UpdateTaskAsync(user.Id, userTask.Id, userTask));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldThrowCannotUpdateTaskIdException()
    {
        var user = new User { Id = Guid.NewGuid(), Name = "Test", Email = "some@email.com" };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new CannotUpdateTaskIdException());

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act and Assert
        await Assert.ThrowsAsync<CannotUpdateTaskIdException>(() =>
            userTaskLogic.UpdateTaskAsync(user.Id, Guid.NewGuid(), userTask));
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldReturnCorrectResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "some@email.com"
        };
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = user.Id
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockUserRepository.Object);

        // Act
        await userTaskLogic.DeleteTaskAsync(user.Id, userTask.Id);

        // Assert
        mockUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
        mockedUserTaskRepository.Verify(x => x.DeleteAsync(It.Is<Guid>(x => x == userTask.Id)), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldThrowUserNotFoundExceptionIfUserIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            IsDone = false,
            UserId = userId
        };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockedUserRepository = new Mock<IUserRepository>();

        mockedUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == userId)));

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(userTask);

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockedUserRepository.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() =>
            userTaskLogic.DeleteTaskAsync(userId, userTask.Id));
        mockedUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == userId)), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldThrowTaskNotFoundExceptionIfTaskIsNull()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Name = "Test", Email = "something@test.com" };

        var mockedUserTaskRepository = new Mock<IUserTaskRepository>();
        var mockedUserRepository = new Mock<IUserRepository>();

        mockedUserRepository
            .Setup(x => x.GetByIdAsync(It.Is<Guid>(u => u == user.Id)))
            .ReturnsAsync(user);

        mockedUserTaskRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var userTaskLogic = new UserTaskLogic(mockedUserTaskRepository.Object, mockedUserRepository.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFoundException>(() =>
            userTaskLogic.DeleteTaskAsync(user.Id, Guid.NewGuid()));
        mockedUserRepository.Verify(x => x.GetByIdAsync(It.Is<Guid>(x => x == user.Id)), Times.Once);
    }
}