using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteService.App.Commands.Trips;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITripRepository _tripRepository;

        public TripController(IMediator mediator, ITripRepository tripRepository)
        {
            _mediator = mediator;
            _tripRepository = tripRepository;
        }

        // GET: api/Trip
        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var trips = await _tripRepository.GetAllAsync();
            return Ok(trips);
        }

        // GET: api/Trip/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(string id)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        // GET: api/Trip/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTrips()
        {
            var trips = await _tripRepository.GetActiveTripsAsync();
            return Ok(trips);
        }

        // POST: api/Trip
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetTripById), new { id = result.Data }, result);
            }
            return BadRequest($"Failed to create trip. Message: {result.Message}");
        }

        // PUT: api/Trip/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTripCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in body.");

            var result = await _mediator.Send(command);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // DELETE: api/Trip/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(string id)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            await _tripRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}