using Microsoft.AspNetCore.Mvc;
using ToDo.Models;

namespace ToDo.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    // GET all for a user
    [HttpGet]
    [Route("{userId}")]
    public Task<IEnumerable<UserTask>> GetAUsersTasks(Guid userId)
    {
        return null;
    }
    
    // Get a single task for a user
    [HttpGet]
    [Route("{userId}/{taskId}")]
    public Task<UserTask> GetAUserTask(Guid userId, Guid taskId)
    {
        return null;
    }
    
    // Create a new task
    [HttpPost]
    [Route("{userId}/create")]
    public Task<UserTask> CreateAUserTask(Guid userId, [FromBody] UserTask task)
    {
        return null;
    }
    
    // Update a task
    [HttpPut]
    [Route("{userId}/{taskId}/update")]
    public Task<UserTask> UpdateAUserTask(Guid userId, Guid taskId, [FromBody] UserTask task)
    {
        return null;
    }
    
    // Delete a task
    [HttpDelete]
    [Route("{userId}/{taskId}/delete")]
    public Task DeleteAUserTask(Guid userId, Guid taskId)
    {
        return null;
    }
}