using backend.DTOs;
using backend.Trip.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Trip;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    
    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    // get all trips
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trips = await _tripService.GetAllAsync();
        return Ok(trips);
    }
    
    // get trip by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdTrip(int id)
    {
        var trip = await _tripService.GetByIdTripAsync(id);
        if (trip == null)
            return NotFound(new { message = "Trip not found." });
        
        return Ok(trip);
    }
    
    // add new trip
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTripDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await _tripService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdTrip), new { idTrip = created.IdTrip }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    // update trip
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTripDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _tripService.UpdateAsync(id, dto);
        if (!updated)
            return NotFound(new { message = "Trip not found." });
        
        return NoContent();
    }
    
    // delete trip
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _tripService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Trip not found." });
        
        return NoContent();
    }
}