// Controllers/TripAuditController.cs

using FuelService.Domain.Entities;
using FuelService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FuelService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripAuditController : ControllerBase
{
    private readonly ITripAuditRepository _repository;

    public TripAuditController(ITripAuditRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtener el último reporte de auditoría de un viaje por ID de viaje.
    /// </summary>
    [HttpGet("{tripId}")]
    public async Task<ActionResult<TripAudit>> GetLatestAudit(string tripId)
    {
        var audit = await _repository.GetByTripIdAsync(tripId);

        if (audit == null)
            return NotFound($"No audit found for Trip ID: {tripId}");

        return Ok(audit);
    }

 [HttpGet("{routeId}/reports")]
public async Task<ActionResult<IEnumerable<TripAudit>>> GetReportsByRoute(
    string routeId,
    [FromQuery] int skip = 0,
    [FromQuery] int limit = 20)
{
    var audits = await _repository.GetReportsAsync(routeId, skip, limit);

    if (audits == null || audits.Length == 0)
        return NotFound($"No reports found for Route ID: {routeId}");

    return Ok(audits);
}
}
