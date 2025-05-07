using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class OfferController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Offers.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Offers.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "OffersAdmin,SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OfferDTO dto)
        {
            var offer = new Offer
            {
                HotelId = dto.HotelId,
                GuideId = dto.GuideId,
                Price = dto.Price,
                Name = dto.Name,
                FlightId = dto.FlightId,
                Details = dto.Details,
                Days = dto.Days,
            };
            try
            {
                await _context.AddAsync(offer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "OffersAdmin,SuperAdmin")]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] OfferDTO dto)
        {
            var offer = await _context.Offers.FindAsync(Id);
            if (offer is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            offer.Price = dto.Price;
            offer.Days = dto.Days;
            offer.Details = dto.Details;
            offer.FlightId = dto.FlightId;
            offer.Name = dto.Name;
            offer.GuideId = dto.GuideId;
            offer.HotelId = dto.HotelId;
            try
            {
                _context.Offers.Update(offer);
                await _context.SaveChangesAsync();
                return Ok(offer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "OffersAdmin,SuperAdmin")]
        [HttpPut("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.Offers.FindAsync(Id);
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
