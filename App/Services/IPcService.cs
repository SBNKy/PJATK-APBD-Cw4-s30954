using App.DTOs;

namespace App.Services;

public interface IPcService
{
    Task<ICollection<PcResponse>> GetAllAsync(CancellationToken cancellationToken);
}