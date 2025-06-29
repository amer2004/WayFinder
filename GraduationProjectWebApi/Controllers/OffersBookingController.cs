using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class OffersBookingController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.OffersBookings.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.OffersBookings.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OffersBookingDTO dto)
        {
            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer is null)
            {
                return BadRequest("The provided flight id dose not correspond to an object");
            }

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user is null)
            {
                return BadRequest("The provided user id dose not correspond to an object");
            }

            var booking = new OffersBooking
            {
                Price = dto.Price,
                BookDate = dto.BookDate,
                OfferId = dto.OfferId,
                UserId = dto.UserId,
            };
            try
            {
                await _context.AddAsync(booking);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] OffersBookingDTO dto)
        {
            var booking = await _context.OffersBookings.FindAsync(Id);
            if (booking is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }

            var offer = await _context.Offers.FindAsync(dto.OfferId);
            if (offer is null)
            {
                return BadRequest("The provided flight id dose not correspond to an object");
            }

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user is null)
            {
                return BadRequest("The provided user id dose not correspond to an object");
            }

            booking.BookDate = dto.BookDate;
            booking.OfferId = dto.OfferId;
            booking.UserId = dto.UserId;
            booking.Price = dto.Price;
            try
            {
                _context.OffersBookings.Update(booking);
                await _context.SaveChangesAsync();
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.OffersBookings.FindAsync(Id);
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
