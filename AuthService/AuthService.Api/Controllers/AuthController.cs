using AuthService.AuthService.Application.Commands.Auth.Login;
using AuthService.AuthService.Application.Common.Constants;
using AuthService.AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.AuthService.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpPost("register")]
        //[Authorize(Roles = RoleConstants.Admin)] // Solo administradores pueden registrar usuarios
        //public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        //{
        //    var userId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(Login), new { id = userId });
        //}

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            return Ok(new
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Username = User.FindFirst(ClaimTypes.Name)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}
