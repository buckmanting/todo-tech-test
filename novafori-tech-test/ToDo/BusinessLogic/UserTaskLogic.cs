using ToDo.BusinessLogic.Interfaces;
using ToDo.Models;

namespace ToDo.BusinessLogic;

public class UserTaskLogic: IUserTaskLogic
{
    /// <summary>
    /// Returns a list of a user tasks
    /// </summary>
    /// <param name="userId">Id of the User to find the tasks of</param>
    /// <returns>A collection of tasks owned by a given user</returns>
    /// <exception cref="UserNotFound">If the user is not found</exception>
    public Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a single task for a given user 
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to return</param>
    /// <returns>The task of a user</returns>
    /// <exception cref="UserNotFound">If the user is not found</exception>
    /// <exception cref="TaskNotFound">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTask">If the user does not own the task</exception>
    public Task<UserTask> GetTaskAsync(Guid userId, Guid taskId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a new task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="task">Task to be created</param>
    /// <returns>The new task</returns>
    /// <exception cref="UserNotFound">If the user is not found</exception>
    public Task<UserTask> CreateTaskAsync(Guid userId, UserTask task)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates an existing task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to update</param>
    /// <param name="task">New details of the task</param>
    /// <returns>Updated task</returns>
    /// <exception cref="UserNotFound">If the user is not found</exception>
    /// <exception cref="TaskNotFound">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTask">If the user does not own the task</exception>
    /// <exception cref="CannotUpdateTaskId">If the task id parameter and task.id properties do not match</exception>
    public Task<UserTask> UpdateTaskAsync(Guid userId, Guid taskId, UserTask task)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes an existing task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to delete</param>
    /// <returns>Void</returns>
    /// <exception cref="UserNotFound">If the user is not found</exception>
    /// <exception cref="TaskNotFound">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTask">If the user does not own the task</exception>
    public Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        throw new NotImplementedException();
    }
}