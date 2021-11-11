namespace DDDExample.Application.Controllers
{
    using System;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Service.Validators;

    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IBaseService<User> _userService;

        public UserController(IBaseService<User> userService) => _userService = userService;

        [HttpGet]
        public IActionResult GetUsers() => Execute(() => _userService.Get());

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            return user is null ? BadRequest(nameof(user)) : Execute(() => _userService.Add<UserValidator>(user));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] User user)
        {
            return Execute(() => _userService.Update<UserValidator>(id, user));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id) => Execute(() => _userService.GetById(id));

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}