using App.DTOs;

namespace App.Services;

public interface IPcService
{
    Task<ICollection<PcResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<PcDetailsResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PcResponse> AddAsync(CreatePcRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(int id, UpdatePcRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}