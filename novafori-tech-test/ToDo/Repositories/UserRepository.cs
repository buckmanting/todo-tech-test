using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Repositories;

public class UserRepository: IUserRepository
{
    private static readonly User StubbedUser = new User{ Id = Guid.NewGuid(), Name = "aaron", Email = "aaron@test.com"};

    public Task<User> GetByIdAsync(Guid id)
    {
        return Task.FromResult(StubbedUser);
    }
}