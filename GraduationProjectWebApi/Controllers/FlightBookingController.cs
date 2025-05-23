using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class FlightBookingController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.FlightBookings.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.FlightBookings.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FlightBookingDTO dto)
        {
            var flight = await _context.Flights.FindAsync(dto.FlightId);
            if (flight is null)
            {
                return BadRequest("The provided flight id dose not correspond to an object");
            }

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user is null)
            {
                return BadRequest("The provided User id dose not correspond to an object");
            }
            if (dto.OfferId is not null)
            {
                var offer = await _context.Offers.FindAsync(dto.OfferId);
                if (offer is null)
                {
                    return BadRequest("The provided offer id dose not correspond to an object");
                }
            }
            var flightBooking = new FlightBooking
            {
                FlightId = dto.FlightId,
                OfferId = dto.OfferId,
                UserId = dto.UserId,
                Price = dto.Price,
                BookDate = dto.BookDate,
            };
            try
            {
                await _context.AddAsync(flightBooking);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id,[FromBody] FlightBookingDTO dto)
        {
            var flightBooking = await _context.FlightBookings.FindAsync(Id);
            if (flightBooking is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }

            var flight = await _context.Flights.FindAsync(dto.FlightId);
            if (flight is null)
            {
                return BadRequest("The provided flight id dose not correspond to an object");
            }

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user is null)
            {
                return BadRequest("The provided User id dose not correspond to an object");
            }

            if (dto.OfferId is not null)
            {
                var offer = await _context.Offers.FindAsync(dto.OfferId);
                if (offer is null)
                {
                    return BadRequest("The provided offer id dose not correspond to an object");
                }
            }

            flightBooking.BookDate = dto.BookDate;
            flightBooking.OfferId = dto.OfferId;
            flightBooking.FlightId = dto.FlightId;
            flightBooking.UserId = dto.UserId;
            flightBooking.Price = dto.Price;
            try
            {
                _context.FlightBookings.Update(flightBooking);
                await _context.SaveChangesAsync();
                return Ok(flightBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.FlightBookings.FindAsync(Id);
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
