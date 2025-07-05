using System.Threading.Tasks;
using FuelService.Domain.Entities;

namespace FuelService.Domain.Interfaces;

public interface ITripAuditRepository
{
    Task InsertAsync(TripAudit audit);
    Task<TripAudit> GetByTripIdAsync(string tripId);
    Task<TripAudit[]> GetReportsAsync(string tripId, int skip = 0, int limit = 100);
    Task UpdateFinalResultAsync(string tripId, FinalResult finalResult);

    
}

