using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class GuideController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Guides.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Guides.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles ="SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] GuideDTO dto)
        {
            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }
            var guide = new Guide
            {
                AdminId = dto.AdminId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate,
                Image=dto.Image,
                Nationality=dto.Nationality,
            };
            try
            {
                await _context.AddAsync(guide);
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
        public async Task<IActionResult> Update(int Id, [FromBody] GuideDTO dto)
        {
            var guide = await _context.Guides.FindAsync(Id);
            if (guide is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }
            guide.AdminId = dto.AdminId;
            guide.LastName = dto.LastName;
            guide.FirstName = dto.FirstName;
            guide.PhoneNumber = dto.PhoneNumber;
            guide.Image = dto.Image;
            guide.Nationality = dto.Nationality;
            guide.BirthDate = dto.BirthDate;
            try
            {
                _context.Guides.Update(guide);
                await _context.SaveChangesAsync();
                return Ok(guide);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.Guides.FindAsync(Id);
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
