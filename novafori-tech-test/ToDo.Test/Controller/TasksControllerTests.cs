using Moq;
using ToDo.BusinessLogic.Interfaces;
using ToDo.Controllers;
using ToDo.Exceptions;
using ToDo.Models;

namespace ToDo.Test;

public class TasksControllerTests
{
    [Fact]
    public async Task GetAUsersTasks_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockUserTasks = new List<UserTask>
        {
            new() { Id = Guid.NewGuid(), UserId = userId, Description = "test" },
            new() { Id = Guid.NewGuid(), UserId = userId, Description = "test" },
            new() { Id = Guid.NewGuid(), UserId = userId, Description = "test" }
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTasksAsync(It.IsAny<Guid>()).Result)
            .Returns(mockUserTasks);

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        var result = await controller.GetAUsersTasks(userId);

        // Assert
        Assert.Equal(result, mockUserTasks);
        mockedBusinessLogic
            .Verify(
                x => x.GetTasksAsync(It.Is<Guid>(x => x == userId)),
                Times.Once);
    }

    [Fact]
    public async Task GetAUsersTasks_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTasksAsync(It.IsAny<Guid>()).Result)
            .Throws(new UserNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await controller.GetAUsersTasks(userId));
        mockedBusinessLogic
            .Verify(
                x => x.GetTasksAsync(It.Is<Guid>(x => x == userId)),
                Times.Once);
    }

    [Fact]
    public async Task GetAUsersTasks_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTasksAsync(It.IsAny<Guid>()).Result)
            .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.GetAUsersTasks(userId));
        mockedBusinessLogic
            .Verify(
                x => x.GetTasksAsync(It.Is<Guid>(x => x == userId)),
                Times.Once);
    }

    [Fact]
    public async Task GetAUserTask_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTaskAsync(userId, taskId).Result)
            .Returns(mockUserTask);

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        var result = await controller.GetAUserTask(userId, taskId);

        // Assert
        Assert.Equal(result, mockUserTask);
        mockedBusinessLogic
            .Verify(
                x => x.GetTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task GetAUserTask_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTaskAsync(userId, taskId).Result)
            .Throws(new UserNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await controller.GetAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.GetTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task GetAUserTask_ReturnsTaskNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTaskAsync(userId, taskId).Result)
            .Throws(new TaskNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFoundException>(async () => await controller.GetAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.GetTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task GetAUserTask_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.GetTaskAsync(userId, taskId).Result)
            .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.GetAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.GetTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task CreateAUserTask_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockNewUserTask = new NewUserTask
        {
            Description = "test"
        };
        var expectedUserTask = new UserTask
        {
            UserId = userId,
            Description = mockNewUserTask.Description
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.CreateTaskAsync(userId, mockNewUserTask).Result)
            .Returns(expectedUserTask);

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        var result = await controller.CreateAUserTask(userId, mockNewUserTask);

        // Assert
        Assert.Equal(result.Description, expectedUserTask.Description);
        Assert.Equal(result.UserId, expectedUserTask.UserId);
        mockedBusinessLogic
            .Verify(
                x => x.CreateTaskAsync(userId, mockNewUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task CreateAUserTask_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockNewUserTask = new NewUserTask
        {
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
           .Setup(x => x.CreateTaskAsync(userId, mockNewUserTask).Result)
           .Throws(new UserNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await controller.CreateAUserTask(userId, mockNewUserTask));
        mockedBusinessLogic
           .Verify(
                x => x.CreateTaskAsync(userId, mockNewUserTask),
                Times.Once);
    }

    [Fact]
    public async Task CreateAUserTask_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockNewUserTask = new NewUserTask
        {
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.CreateTaskAsync(userId, mockNewUserTask).Result)
            .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.CreateAUserTask(userId, mockNewUserTask));
        mockedBusinessLogic
            .Verify(
                x => x.CreateTaskAsync(userId, mockNewUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task UpdateAUserTask_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
           .Setup(x => x.UpdateTaskAsync(userId, taskId, mockUserTask).Result)
           .Returns(mockUserTask);

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        var result = await controller.UpdateAUserTask(userId, taskId, mockUserTask);

        // Assert
        Assert.Equal(result, mockUserTask);
        mockedBusinessLogic
           .Verify(
                x => x.UpdateTaskAsync(userId, taskId, mockUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task UpdateAUserTask_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
          .Setup(x => x.UpdateTaskAsync(userId, taskId, mockUserTask).Result)
          .Throws(new UserNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
        mockedBusinessLogic
          .Verify(
                x => x.UpdateTaskAsync(userId, taskId, mockUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task UpdateAUserTask_ReturnsTaskNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
         .Setup(x => x.UpdateTaskAsync(userId, taskId, mockUserTask).Result)
         .Throws(new TaskNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFoundException>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
        mockedBusinessLogic
         .Verify(
                x => x.UpdateTaskAsync(userId, taskId, mockUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task UpdateAUserTask_ReturnsCannotUpdateTaskId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
        .Setup(x => x.UpdateTaskAsync(userId, taskId, mockUserTask).Result)
        .Throws(new CannotUpdateTaskIdException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<CannotUpdateTaskIdException>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
        mockedBusinessLogic
        .Verify(
                x => x.UpdateTaskAsync(userId, taskId, mockUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task UpdateAUserTask_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            Id = taskId,
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
          .Setup(x => x.UpdateTaskAsync(userId, taskId, mockUserTask).Result)
          .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
        mockedBusinessLogic
          .Verify(
                x => x.UpdateTaskAsync(userId, taskId, mockUserTask),
                Times.Once);
    }

    [Fact]
    public async Task DeleteAUserTask_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.DeleteTaskAsync(userId, taskId));

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        await controller.DeleteAUserTask(userId, taskId);

        // Assert
        mockedBusinessLogic
            .Verify(
                x => x.DeleteTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task DeleteAUserTask_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.DeleteTaskAsync(userId, taskId))
            .Throws(new UserNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await controller.DeleteAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.DeleteTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task DeleteAUserTask_ReturnsTaskNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.DeleteTaskAsync(userId, taskId))
            .Throws(new TaskNotFoundException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFoundException>(async () => await controller.DeleteAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.DeleteTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task DeleteAUserTask_ReturnsUserDoesNotOwnTask()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.DeleteTaskAsync(userId, taskId))
            .Throws(new UserDoesNotOwnTaskException());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserDoesNotOwnTaskException>(async () => await controller.DeleteAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.DeleteTaskAsync(userId, taskId),
                Times.Once);
    }

    [Fact]
    public async Task DeleteAUserTask_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.DeleteTaskAsync(userId, taskId))
            .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.DeleteAUserTask(userId, taskId));
        mockedBusinessLogic
            .Verify(
                x => x.DeleteTaskAsync(userId, taskId),
                Times.Once);
    }
}