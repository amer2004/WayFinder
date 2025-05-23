using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class LocationController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Locations.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Locations.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] LocationDTO dto)
        {
            var location=new Location
            {
                Country=dto.Country,
                Description=dto.Description,
                Name=dto.Name,
                Image=dto.Image,
            };
            try
            {
                await _context.AddAsync(location);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id,[FromBody] LocationDTO dto)
        {
            var location = await _context.Locations.FindAsync(Id);
            if (location is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            location.Description = dto.Description;
            location.Name = dto.Name;
            location.Country = dto.Country;
            location.Image = dto.Image;
            try
            {
                _context.Locations.Update(location);
                await _context.SaveChangesAsync();
                return Ok(location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.Locations.FindAsync(Id);
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
