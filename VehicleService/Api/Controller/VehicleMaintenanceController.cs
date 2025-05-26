using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleService.App.Commands.VehicleMaintenanceCommand.Update;
using VehicleService.App.Commands.VehicleMaintenanceCommand;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class VehicleMaintenanceController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IVehicleMaintenance _vehicleMaintenanceRepository;

            public VehicleMaintenanceController(IMediator mediator, IVehicleMaintenance vehicleMaintenanceRepository)
            {
                _mediator = mediator;
                _vehicleMaintenanceRepository = vehicleMaintenanceRepository;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var maintenances = await _vehicleMaintenanceRepository.GetAllAsync();
                return Ok(maintenances);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(string id)
            {
                var maintenance = await _vehicleMaintenanceRepository.GetByIdAsync(id);
                if (maintenance == null) return NotFound();
                return Ok(maintenance);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateVehicleMaintenanceCommand command)
            {
                var result = await _mediator.Send(command);
                if (result.Success)
                {
                    return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
                }
                return Conflict(result.Message ?? "Error al crear el mantenimiento.");
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(string id, [FromBody] UpdateVehicleMaintenanceCommand command)
            {
                if (id != command.Id)
                    return BadRequest("El ID de la ruta no coincide con el del cuerpo.");

                var result = await _mediator.Send(command);
                if (!result.Success)
                    return NotFound(result);

                return Ok(result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(string id)
            {
                var existing = await _vehicleMaintenanceRepository.GetByIdAsync(id);
                if (existing == null) return NotFound();

                await _vehicleMaintenanceRepository.DeleteAsync(id);
                return NoContent();
            }

            [HttpGet("by-vehicle/{vehicleId}")]
            public async Task<IActionResult> GetByVehicleId(string vehicleId)
            {
                var maintenances = await _vehicleMaintenanceRepository.GetByVehicleIdAsync(vehicleId);
                return Ok(maintenances);
            }

            [HttpGet("completed/{vehicleId}")]
            public async Task<IActionResult> GetCompletedMaintenances(string vehicleId)
            {
                var completed = await _vehicleMaintenanceRepository.GetCompletedMaintenancesAsync(vehicleId);
                return Ok(completed);
            }

            [HttpGet("ongoing")]
            public async Task<IActionResult> GetOngoingMaintenances()
            {
                var ongoing = await _vehicleMaintenanceRepository.GetOngoingMaintenancesAsync();
                return Ok(ongoing);
            }
        }
    
}
