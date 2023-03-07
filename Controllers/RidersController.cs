using FormulaOne.Server.Data;
using FormulaOne.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RidersController : ControllerBase
{
    private readonly ApiDbContext _context;
    
    public RidersController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rider>>> GetRiders()
    {
        var riders = await _context.Riders.ToListAsync(); 
        return Ok(riders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rider>> GetRiderDetails(int id)
    {
        var rider = await _context.Riders.FirstOrDefaultAsync(x => x.Id == id);

        if(rider == null)
            return NotFound();

        return Ok(rider);

    }

    [HttpPost]
    public async Task<IActionResult> CreateRider_(Rider rider)
    {
        _context.Riders.Add(rider);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRiderDetails), rider, rider.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRider(Rider rider, int id)
    {
        var riderExist = await _context.Riders.FirstOrDefaultAsync(x => x.Id == id);

        if(riderExist == null)
            return NotFound();

        riderExist.Name = rider.Name;
        riderExist.RacingNb = rider.RacingNb;
        riderExist.Team = rider.Team;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRider(int id)
    {
        var riderExist = await _context.Riders.FirstOrDefaultAsync(x => x.Id == id);

        if (riderExist == null)
            return NotFound();

        _context.Riders.Remove(riderExist);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}