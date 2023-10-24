using ToDo.BusinessLogic.Interfaces;
using ToDo.Models;

namespace ToDo.BusinessLogic;

public class UserTaskLogic: IUserTaskLogic
{
    public Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> GetTaskAsync(Guid userId, Guid taskId)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> CreateTaskAsync(Guid userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> UpdateTaskAsync(Guid userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTask(Guid userId, Guid taskId)
    {
        throw new NotImplementedException();
    }
}