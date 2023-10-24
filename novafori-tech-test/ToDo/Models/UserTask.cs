namespace ToDo.Models;

public class UserTask
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }
}