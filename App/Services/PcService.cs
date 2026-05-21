using App.DTOs;
using App.Exceptions;
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

    public async Task<PcDetailsResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await ctx.Pcs
                   .Where(pc => pc.Id == id)
                   .Select(pc => new PcDetailsResponse(
                       pc.Id,
                       pc.Name,
                       pc.Weight,
                       pc.Warranty,
                       pc.CreatedAt,
                       pc.Stock,
                       pc.PcComponents.Select(comp => new PcComponentResponse(
                           comp.Amount,
                           new ComponentResponse(
                               comp.Component.Code,
                               comp.Component.Name,
                               comp.Component.Description,
                               new ManufacturerResponse(
                                   comp.Component.ComponentManufacturer.Id,
                                   comp.Component.ComponentManufacturer.Abbreviation,
                                   comp.Component.ComponentManufacturer.FullName,
                                   comp.Component.ComponentManufacturer.FoundationDate
                               ),
                               new ComponentTypeResponse(
                                   comp.Component.ComponentType.Id,
                                   comp.Component.ComponentType.Abbreviation,
                                   comp.Component.ComponentType.Name
                               )
                           )
                       )).ToList()
                   )).FirstOrDefaultAsync(cancellationToken)
               ?? throw new PcNotFoundException($"PC with id {id} not found");
    }
}