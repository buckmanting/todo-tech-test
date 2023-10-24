using ToDo.Models;

namespace ToDo.BusinessLogic.Interfaces;

public interface IUserTaskLogic
{
    Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId);
    Task<UserTask> GetTaskAsync(Guid userId, Guid taskId);
    Task<UserTask> CreateTaskAsync(Guid userId, UserTask task);
    Task<UserTask> UpdateTaskAsync(Guid userId, UserTask task);
    Task DeleteTask(Guid userId, Guid taskId);
}