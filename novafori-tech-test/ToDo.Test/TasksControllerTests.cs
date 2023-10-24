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
            .Throws(new UserNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFound>(async () => await controller.GetAUsersTasks(userId));
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
            .Throws(new UserNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFound>(async () => await controller.GetAUserTask(userId, taskId));
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
            .Throws(new TaskNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFound>(async () => await controller.GetAUserTask(userId, taskId));
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
        var mockUserTask = new UserTask
        {
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.CreateTaskAsync(userId, mockUserTask).Result)
            .Returns(mockUserTask);

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act
        var result = await controller.CreateAUserTask(userId, mockUserTask);

        // Assert
        Assert.Equal(result, mockUserTask);
        mockedBusinessLogic
            .Verify(
                x => x.CreateTaskAsync(userId, mockUserTask),
                Times.Once);
    }
    
    [Fact]
    public async Task CreateAUserTask_ReturnsUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
           .Setup(x => x.CreateTaskAsync(userId, mockUserTask).Result)
           .Throws(new UserNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFound>(async () => await controller.CreateAUserTask(userId, mockUserTask));
        mockedBusinessLogic
           .Verify(
                x => x.CreateTaskAsync(userId, mockUserTask),
                Times.Once);
    }

    [Fact]
    public async Task CreateAUserTask_ReturnsOtherThrownError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockUserTask = new UserTask
        {
            UserId = userId,
            Description = "test"
        };
        var mockedBusinessLogic = new Mock<IUserTaskLogic>();
        mockedBusinessLogic
            .Setup(x => x.CreateTaskAsync(userId, mockUserTask).Result)
            .Throws(new Exception());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await controller.CreateAUserTask(userId, mockUserTask));
        mockedBusinessLogic
            .Verify(
                x => x.CreateTaskAsync(userId, mockUserTask),
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
          .Throws(new UserNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFound>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
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
         .Throws(new TaskNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFound>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
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
        .Throws(new CannotUpdateTaskId());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<CannotUpdateTaskId>(async () => await controller.UpdateAUserTask(userId, taskId, mockUserTask));
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
            .Throws(new UserNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserNotFound>(async () => await controller.DeleteAUserTask(userId, taskId));
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
            .Throws(new TaskNotFound());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<TaskNotFound>(async () => await controller.DeleteAUserTask(userId, taskId));
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
            .Throws(new UserDoesNotOwnTask());

        var controller = new TasksController(mockedBusinessLogic.Object);

        // Act and Assert
        await Assert.ThrowsAsync<UserDoesNotOwnTask>(async () => await controller.DeleteAUserTask(userId, taskId));
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