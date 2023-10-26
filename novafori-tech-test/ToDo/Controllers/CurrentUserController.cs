using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessLogic.Interfaces;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Repositories.Interfaces;

namespace ToDo.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrentUserController : ControllerBase
{
    private IUserRepository _userRepository;

    public CurrentUserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<User> Index()
    {
        // todo: this is janky, we need to pas the userId here as we don't have a cookie to store it in on te client
        // if we did we could return the stubbed out data
        return await _userRepository.GetByIdAsync(Guid.NewGuid());
    }
}