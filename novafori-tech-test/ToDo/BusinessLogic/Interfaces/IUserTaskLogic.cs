using ToDo.Models;

namespace ToDo.BusinessLogic.Interfaces;

public interface IUserTaskLogic
{
    Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId);
    Task<UserTask> GetTaskAsync(Guid userId, Guid taskId);
    Task<UserTask> CreateTaskAsync(Guid userId, NewUserTask task);
    Task<UserTask> UpdateTaskAsync(Guid userId, Guid taskId, UserTask task);
    Task DeleteTaskAsync(Guid userId, Guid taskId);
}