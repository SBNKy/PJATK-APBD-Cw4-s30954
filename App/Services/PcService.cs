using App.DTOs;
using App.Exceptions;
using App.Infrastructure;
using App.Models;
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

    public async Task<PcResponse> AddAsync(CreatePcRequest request, CancellationToken cancellationToken)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = DateTime.UtcNow,
            Stock = request.Stock
        };

        ctx.Add(pc);
        await ctx.SaveChangesAsync(cancellationToken);

        return new PcResponse(pc.Id, pc.Name, pc.Weight, pc.Warranty, pc.CreatedAt, pc.Stock);
    }

    public async Task UpdateAsync(int id, UpdatePcRequest request, CancellationToken cancellationToken)
    {
        var affectedRows = await ctx.Pcs.Where(pc => pc.Id == id)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(pc => pc.Name, request.Name)
                    .SetProperty(pc => pc.Weight, request.Weight)
                    .SetProperty(pc => pc.Warranty, request.Warranty)
                    .SetProperty(pc => pc.Stock, request.Stock),
                cancellationToken
            );

        if (affectedRows == 0)
        {
            throw new PcNotFoundException($"PC with id {id} not found.");
        }
    }
}