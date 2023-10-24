using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessLogic.Interfaces;
using ToDo.Exceptions;
using ToDo.Models;

namespace ToDo.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private IUserTaskLogic _userTaskLogic;

    public TasksController(IUserTaskLogic userTaskLogic)
    {
        _userTaskLogic = userTaskLogic;
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IEnumerable<UserTask>> GetAUsersTasks(Guid userId)
    {
        try
        {
            return await _userTaskLogic.GetTasksAsync(userId);
        }
        catch (UserNotFound ex)
        {
            // return a not found message
            throw ex;
        }
        catch (Exception ex)
        {
            //likely a server error at this point
            // todo: return a 500 error message
            throw ex;
        }

        // todo remove this once exceptions are caught and handled
        return null;
    }

    [HttpGet]
    [Route("{userId}/{taskId}")]
    public async Task<UserTask> GetAUserTask(Guid userId, Guid taskId)
    {
        try
        {
            return await _userTaskLogic.GetTaskAsync(userId, taskId);
        }
        catch (UserNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (TaskNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (Exception ex)
        {
            //likely a server error at this point
            // todo: return a 500 error message
            throw ex;
        }
    }

    [HttpPost]
    [Route("{userId}/create")]
    public async Task<UserTask> CreateAUserTask(Guid userId, [FromBody] UserTask task)
    {
        try
        {
            return await _userTaskLogic.CreateTaskAsync(userId, task);
        }
        catch (UserNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (Exception ex)
        {
            //likely a server error at this point
            // todo return correct status code rather than rethrowing
            throw ex;
        }
    }

    [HttpPut]
    [Route("{userId}/{taskId}/update")]
    public async Task<UserTask> UpdateAUserTask(Guid userId, Guid taskId, [FromBody] UserTask task)
    {
        try
        {
            return await _userTaskLogic.UpdateTaskAsync(userId, taskId, task);
        }
        catch (UserNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (TaskNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (UserDoesNotOwnTask ex)
        {
            // return a not allowed error message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (CannotUpdateTaskId ex)
        {
            // return a not allowed error message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (Exception ex)
        {
            //likely a server error at this point
            // todo: return a 500 error message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        return null;
    }

    [HttpDelete]
    [Route("{userId}/{taskId}/delete")]
    public async Task DeleteAUserTask(Guid userId, Guid taskId)
    {
        try
        {
            await _userTaskLogic.DeleteTaskAsync(userId, taskId);
        }
        catch (UserNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (UserDoesNotOwnTask ex)
        {
            // return a not allowed error message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (TaskNotFound ex)
        {
            // return a not found message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
        catch (Exception ex)
        {
            //likely a server error at this point
            // todo: return a 500 error message
            // todo return correct status code rather than rethrowing
            throw ex;
        }
    }
}