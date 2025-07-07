using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DriverServices.App.Commands;
using DriverServices.Domain.Interfaces.Repositories;
using DriverServices.App.Commands.UpdateDriver;

namespace DriverServices.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere autenticación para todos los endpoints
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDriverRepository _driverRepository;

        public DriverController(IMediator mediator, IDriverRepository driverRepository)
        {
            _mediator = mediator;
            _driverRepository = driverRepository;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return Ok(drivers);
        }

        // GET: api/Driver/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverById(string id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        // GET: api/Driver/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableDrivers()
        {
            var drivers = await _driverRepository.GetAvailableDriversAsync();
            return Ok(drivers);
        }

        // POST: api/Driver
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")] // Solo admins y managers pueden crear conductores
        public async Task<IActionResult> CreateDriver([FromBody] CreateDriverRegistrationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetDriverById), new { data = result }, result);
            }
            return BadRequest("Failed to create driver.");
        }

        // PUT: api/Driver/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")] // Solo admins y managers pueden actualizar conductores
        public async Task<IActionResult> Update(string id, [FromBody] UpdateDriverCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in body.");

            var result = await _mediator.Send(command);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    

        // DELETE: api/Driver/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            await _driverRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
