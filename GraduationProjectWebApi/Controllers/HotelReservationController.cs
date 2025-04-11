using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class HotelReservationController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.HotelReservations.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.HotelReservations.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] HotelReservationDTO dto)
        {
            var hotel = new HotelReservation
            {
                Days = dto.Days,
                Price = dto.Price,
                RoomId = dto.RoomId,
                OfferId = dto.OfferId,
            };
            try
            {
                await _context.AddAsync(hotel);
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
        public async Task<IActionResult> Update(int Id, [FromBody] HotelReservationDTO dto)
        {
            var hotel = await _context.HotelReservations.FindAsync(Id);
            if (hotel is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            hotel.OfferId = dto.OfferId;
            hotel.RoomId = dto.RoomId;
            hotel.Price = dto.Price;
            hotel.Days = dto.Days;
            try
            {
                _context.HotelReservations.Update(hotel);
                await _context.SaveChangesAsync();
                return Ok(hotel);
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
            var entity = await _context.HotelReservations.FindAsync(Id);
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
