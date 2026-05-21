using App.DTOs;
using App.Exceptions;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcController(IPcService pcService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await pcService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await pcService.GetByIdAsync(id, cancellationToken));
        }
        catch (PcNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePcRequest request, CancellationToken cancellationToken)
    {
        var pc = await pcService.AddAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = pc.Id}, pc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePcRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await pcService.UpdateAsync(id, request, cancellationToken);
            return NoContent();
        }
        catch (PcNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok();
    }
    
}