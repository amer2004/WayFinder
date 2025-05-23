using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class FlightController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Flights.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Flights.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "FilghtAdmin,SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FlightDTO dto)
        {
            var flight = new Flight
            {
                Number = dto.Number,
                AirLineId = dto.AirLineId,
                DepartureLocationId = dto.DepartureLocationId,
                DestinationLocationId = dto.DestinationLocationId,
                ArrivalTime = dto.ArrivalTime,
                Departure = dto.Departure,
            };
            try
            {
                await _context.AddAsync(flight);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "FilghtAdmin,SuperAdmin")]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] FlightDTO dto)
        {
            var flight = await _context.Flights.FindAsync(Id);
            if (flight is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }

            var DepartureLocation = await _context.Locations.FindAsync(dto.DepartureLocationId);
            if (DepartureLocation is null)
            {
                return BadRequest("The provided Departure Location id dose not correspond to an object");
            }

            var DestinationLocation = await _context.Locations.FindAsync(dto.DestinationLocationId);
            if (DestinationLocation is null)
            {
                return BadRequest("The provided Destination Location id dose not correspond to an object");
            }


            var airLine = await _context.AirLines.FindAsync(dto.AirLineId);
            if (airLine is null)
            {
                return BadRequest("The provided air line id dose not correspond to an object");
            }

            flight.Number = dto.Number;
            flight.AirLineId = dto.AirLineId;
            flight.ArrivalTime = dto.ArrivalTime;
            flight.Departure = dto.Departure;
            flight.DepartureLocationId = dto.DepartureLocationId;
            flight.DestinationLocationId = dto.DestinationLocationId;
            try
            {
                _context.Flights.Update(flight);
                await _context.SaveChangesAsync();
                return Ok(flight);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "FilghtAdmin,SuperAdmin")]
        [HttpPut("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.Flights.FindAsync(Id);
            if (entity is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
