using App.DTOs;
using App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Services;

public class PcService(DatabaseContext ctx) : IPcService
{
    public async Task<ICollection<PcResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await ctx.Pcs.Select(pc => new PcResponse(
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        )).ToListAsync(cancellationToken);
    }
}