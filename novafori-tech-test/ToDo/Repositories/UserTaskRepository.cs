using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Repositories;

class UserTaskRepository : IUserTaskRepository
{
    public Task<IEnumerable<UserTask>> GetAllAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> GetAsync(string userId, string taskId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> CreateAsync(string userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> UpdateAsync(string userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string userId, string taskId)
    {
        throw new NotImplementedException();
    }
}