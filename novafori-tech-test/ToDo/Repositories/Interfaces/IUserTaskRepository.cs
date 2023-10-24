using ToDo.Models;

namespace ToDo.Repositories.Interfaces;

public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetAllAsync(string userId);
    Task<UserTask> GetAsync(string userId, string taskId);
    Task<UserTask> CreateAsync(string userId, UserTask task);
    Task<UserTask> UpdateAsync(string userId, UserTask task);
    Task DeleteAsync(string userId, string taskId);
}