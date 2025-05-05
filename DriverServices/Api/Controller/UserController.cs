using DriverServices.App.Interfaces;
using DriverServices.Infrastructure.Services.Client;
using Microsoft.AspNetCore.Mvc;

namespace DriverServices.Api.Controller
{

        [ApiController]
        [Route("api/[controller]")]
        public class UserController : ControllerBase
        {
            private readonly IUserClient _userClient;
            private readonly ILogger<UserController> _logger;

            public UserController(IUserClient userClient, ILogger<UserController> logger)
            {
                _userClient = userClient;
                _logger = logger;
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetUser(string id)
            {
                try
                {
                    _logger.LogInformation($"Solicitando información del usuario con ID: {id}");

                    var user = await _userClient.GetUserAsync(id);

                    if (user == null)
                    {
                        return NotFound($"Usuario con ID {id} no encontrado");
                    }

                    return Ok(user);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al obtener información del usuario con ID: {id}");
                    return StatusCode(500, "Error interno al procesar la solicitud");
                }
            }
        
    }
}
