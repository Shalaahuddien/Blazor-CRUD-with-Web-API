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

}