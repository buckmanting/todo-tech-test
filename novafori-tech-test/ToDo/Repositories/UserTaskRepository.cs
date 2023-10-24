using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Repositories;

class UserTaskRepository : IUserTaskRepository
{
    public Task<IEnumerable<UserTask>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> GetByIdAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> CreateAsync(Guid userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> UpdateAsync(UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }
}