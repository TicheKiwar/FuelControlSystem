using Common.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteService.App.Commands.Routes;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRouteRepository _routeRepository;

        public RouteController(IMediator mediator, IRouteRepository routeRepository)
        {
            _mediator = mediator;
            _routeRepository = routeRepository;
        }

        // GET: api/Route
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var routes = await _routeRepository.GetAllAsync();
            return Ok(routes);
        }

        // GET: api/Route/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var route = await _routeRepository.GetByIdAsync(id);
            if (route == null) return NotFound();
            return Ok(route);
        }

        // POST: api/Route
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
            }
            return Conflict(result.Message ?? "Error creating route.");
        }

        // PUT: api/Route/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRouteCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in body.");

            var result = await _mediator.Send(command);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // DELETE: api/Route/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingRoute = await _routeRepository.GetByIdAsync(id);
            if (existingRoute == null) return NotFound();

            await _routeRepository.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/Route/search-by-location
        [HttpPost("search-by-location")]
        public async Task<IActionResult> GetByLocation([FromBody] Location location)
        {
            var routes = await _routeRepository.GetRoutesByLocationAsync(location);
            return Ok(routes);
        }
    }

}