using ToDo.BusinessLogic.Interfaces;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.BusinessLogic;

public class UserTaskLogic : IUserTaskLogic
{
    private IUserTaskRepository _userTaskRepository;
    private IUserRepository _userRepository;

    public UserTaskLogic(IUserTaskRepository userTaskRepository, IUserRepository userRepository)
    {
        _userTaskRepository = userTaskRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Returns a list of a user tasks
    /// </summary>
    /// <param name="userId">Id of the User to find the tasks of</param>
    /// <returns>A collection of tasks owned by a given user</returns>
    /// <exception cref="UserNotFoundException">If the user is not found</exception>
    public async Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return await _userTaskRepository.GetAllAsync(userId);
    }

    /// <summary>
    /// Returns a single task for a given user 
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to return</param>
    /// <returns>The task of a user</returns>
    /// <exception cref="UserNotFoundException">If the user is not found</exception>
    /// <exception cref="TaskNotFoundException">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTaskException">If the user does not own the task</exception>
    public async Task<UserTask> GetTaskAsync(Guid userId, Guid taskId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var userTask = await _userTaskRepository.GetByIdAsync(taskId);

        if (userTask == null)
        {
            throw new TaskNotFoundException();
        }

        if (userTask.UserId != userId)
        {
            throw new UserDoesNotOwnTaskException();
        }

        return userTask;
    }

    /// <summary>
    /// Creates a new task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="task">Task to be created</param>
    /// <returns>The new task</returns>
    /// <exception cref="UserNotFoundException">If the user is not found</exception>
    public async Task<UserTask> CreateTaskAsync(Guid userId, NewUserTask task)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var newTask = new UserTask
        {
            Description = task.Description
        };

        return await _userTaskRepository.CreateAsync(user.Id, newTask);
    }

    /// <summary>
    /// Updates an existing task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to update</param>
    /// <param name="task">New details of the task</param>
    /// <returns>Updated task</returns>
    /// <exception cref="UserNotFoundException">If the user is not found</exception>
    /// <exception cref="TaskNotFoundException">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTaskException">If the user does not own the task</exception>
    /// <exception cref="CannotUpdateTaskIdException">If the task id parameter and task.id properties do not match</exception>
    public async Task<UserTask> UpdateTaskAsync(Guid userId, Guid taskId, UserTask updatedTask)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        
        var userTask = await _userTaskRepository.GetByIdAsync(taskId);
        if (userTask == null)
        {
            throw new TaskNotFoundException();
        }
        
        if (userTask.UserId!= userId)
        {
            throw new UserDoesNotOwnTaskException();
        }
        
        if (updatedTask.Id!= taskId)
        {
            throw new CannotUpdateTaskIdException();
        }
        
        return await _userTaskRepository.UpdateAsync(updatedTask);
    }

    /// <summary>
    /// Deletes an existing task for a given user
    /// </summary>
    /// <param name="userId">Id of the User who owns the task</param>
    /// <param name="taskId">Id of the task to delete</param>
    /// <returns>Void</returns>
    /// <exception cref="UserNotFoundException">If the user is not found</exception>
    /// <exception cref="TaskNotFoundException">If the task is not found</exception>
    /// <exception cref="UserDoesNotOwnTaskException">If the user does not own the task</exception>
    public async Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        
        var userTask = await _userTaskRepository.GetByIdAsync(taskId);
        if (userTask == null)
        {
            throw new TaskNotFoundException();
        }
        
        if (userTask.UserId!= userId)
        {
            throw new UserDoesNotOwnTaskException();
        }
        
        await _userTaskRepository.DeleteAsync(userTask.Id);
    }
}