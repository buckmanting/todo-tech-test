using ToDo.Models;

namespace ToDo.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
}