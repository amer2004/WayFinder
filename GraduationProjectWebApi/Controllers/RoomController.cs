using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class RoomController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Rooms.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Rooms.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "HotelAdmin,SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] RoomDTO dto)
        {
            var hotel = await _context.Hotels.FindAsync(dto.HotelId);
            if (hotel is null)
            {
                return BadRequest("The provided hotel id dose not correspond to an object");
            }
            var type = await _context.RoomTypes.FindAsync(dto.TypeId);
            if (type is null)
            {
                return BadRequest("The provided room type id dose not correspond to an object");
            }
            var room = new Room
            {
                HotelId = dto.HotelId,
                Price = dto.Price,
                Image1 = dto.Image1,
                Image2 = dto.Image2,
                Image3 = dto.Image3,
                Number = dto.Number,
                Status = dto.Status,
                TypeId = dto.TypeId,
            };
            try
            {
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "HotelAdmin,SuperAdmin")]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] RoomDTO dto)
        {
            var room = await _context.Rooms.FindAsync(Id);
            if (room is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            var hotel = await _context.Hotels.FindAsync(dto.HotelId);
            if (hotel is null)
            {
                return BadRequest("The provided hotel id dose not correspond to an object");
            }
            var type = await _context.RoomTypes.FindAsync(dto.TypeId);
            if (type is null)
            {
                return BadRequest("The provided room type id dose not correspond to an object");
            }
            room.Image1 = dto.Image1;
            room.Image2 = dto.Image2;
            room.Image3 = dto.Image3;
            room.Number = dto.Number;
            room.Status = dto.Status;
            room.TypeId = dto.TypeId;
            room.Price = dto.Price;
            room.HotelId = dto.HotelId;
            try
            {
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "HotelAdmin,SuperAdmin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.Rooms.FindAsync(Id);
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
