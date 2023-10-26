using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Repositories;

class UserTaskRepository : IUserTaskRepository
{
    private List<UserTask> _userTasks = new();

    public Task<IEnumerable<UserTask>> GetAllAsync(Guid userId)
    {
        return Task.FromResult<IEnumerable<UserTask>>(_userTasks);
    }

    public Task<UserTask?> GetByIdAsync(Guid taskId)
    {
        return Task.FromResult(_userTasks.FirstOrDefault(x => x.Id == taskId));
    }

    public Task<UserTask> CreateAsync(Guid userId, UserTask task)
    {
        var newUserTask = new UserTask
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Description = task.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDone = false
        };

        _userTasks.Add(newUserTask);

        return Task.FromResult(newUserTask);
    }

    public Task<UserTask> UpdateAsync(UserTask task)
    {
        var userTask = _userTasks.FirstOrDefault(x => x.Id == task.Id);

        if (userTask == null)
        {
            return Task.FromResult(userTask);
        }

        userTask.Description = task.Description;
        userTask.IsDone = task.IsDone;
        userTask.UpdatedAt = DateTime.Now;

        return Task.FromResult(userTask);
    }

    public Task DeleteAsync(Guid taskId)
    {
        var userTask = _userTasks.FirstOrDefault(x => x.Id == taskId);

        if (userTask == null)
        {
            return Task.CompletedTask;
        }

        _userTasks.Remove(userTask);

        return Task.CompletedTask;
    }
}