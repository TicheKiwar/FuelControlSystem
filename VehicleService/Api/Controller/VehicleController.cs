using Common.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleService.App.Commands.Create;
using VehicleService.App.Commands.Update;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
         private readonly IMediator _mediator;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IMediator mediator, IVehicleRepository vehicleRepository)
        {
            _mediator = mediator;
            _vehicleRepository = vehicleRepository;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return Ok(vehicles);
        }

        // GET: api/Vehicle/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound();
            return Ok(vehicle);
        }

        // POST: api/Vehicle
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
            }
            return BadRequest("Failed to create vehicle.");
        }

        // PUT: api/Vehicle/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateVehicleCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in body.");

            var result = await _mediator.Send(command);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // DELETE: api/Vehicle/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            await _vehicleRepository.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/Vehicle/brand/{brand}/model/{model}
        [HttpGet("brand/{brand}/model/{model}")]
        public async Task<IActionResult> FilterByBrandAndModel(string brand, string model)
        {
            var vehicles = await _vehicleRepository.FilterByBrandAndModelAsync(brand, model);
            return Ok(vehicles);
        }

        // GET: api/Vehicle/fuel-type/{fuelType}
        [HttpGet("fuel-type/{fuelType}")]
        public async Task<IActionResult> GetByFuelType(FuelType fuelType)
        {
            var vehicles = await _vehicleRepository.GetByFuelTypeAsync(fuelType);
            return Ok(vehicles);
        }

        // GET: api/Vehicle/type/{type}
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(VehicleType type)
        {
            var vehicles = await _vehicleRepository.GetByTypeAsync(type);
            return Ok(vehicles);
        }
    }
    }
