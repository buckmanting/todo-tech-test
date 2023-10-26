using ToDo.Models;

namespace ToDo.Repositories.Interfaces;

public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetAllAsync(Guid userId);
    Task<UserTask?> GetByIdAsync(Guid taskId);
    Task<UserTask> CreateAsync(Guid userId, UserTask task);
    Task<UserTask> UpdateAsync(UserTask task);
    Task DeleteAsync(Guid taskId);
}