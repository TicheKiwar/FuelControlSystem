using AuthService.AuthService.Application.Commands.UserCommands;
using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserCommand command)
        {
            if (await _userRepository.ExistsByEmailAsync(command.Email))
            {
                return Conflict("El email ya está registrado");
            }

            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = response.Data.Id },
                response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userRepository.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{userId}/roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles(string userId, [FromBody] UpdateRolesRequest request)
        {
            // Implementación que actualiza todos los roles a la vez
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return NotFound();

            user.Roles = request.Roles;
            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpGet("by-role/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(UserRole role)
        {
            var users = await _userRepository.GetUsersByRoleAsync(role);
            return Ok(users);
        }
    }

    public class UpdateRolesRequest
    {
        public List<UserRole> Roles { get; set; }
    }
}
