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
            if (dto.FlightId is not null)
            {
                var flight = await _context.Flights.FindAsync(dto.FlightId);
                if (flight is null)
                {
                    return BadRequest("The provided flight id dose not correspond to an object");
                }
            }

            if (dto.GuideId is not null)
            {
                var Guide = await _context.Guides.FindAsync(dto.GuideId);
                if (Guide is null)
                {
                    return BadRequest("The provided Guide id dose not correspond to an object");
                }
            }

            if (dto.HotelId is not null)
            {
                var Hotel = await _context.Hotels.FindAsync(dto.HotelId);
                if (Hotel is null)
                {
                    return BadRequest("The provided Hotel id dose not correspond to an object");
                }
            }

            var offer = new Offer
            {
                HotelId = dto.HotelId,
                GuideId = dto.GuideId,
                Price = dto.Price,
                Name = dto.Name,
                Image1 = dto.Image1,
                Image2 = dto.Image2,
                Image3 = dto.Image3,
                Image4 = dto.Image4,
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

            if (dto.FlightId is not null)
            {
                var flight = await _context.Flights.FindAsync(dto.FlightId);
                if (flight is null)
                {
                    return BadRequest("The provided flight id dose not correspond to an object");
                }
            }

            if (dto.GuideId is not null)
            {
                var Guide = await _context.Guides.FindAsync(dto.GuideId);
                if (Guide is null)
                {
                    return BadRequest("The provided Guide id dose not correspond to an object");
                }
            }

            if (dto.HotelId is not null)
            {
                var Hotel = await _context.Hotels.FindAsync(dto.HotelId);
                if (Hotel is null)
                {
                    return BadRequest("The provided Hotel id dose not correspond to an object");
                }
            }

            offer.Image1 = dto.Image1;
            offer.Image2 = dto.Image2;
            offer.Image3 = dto.Image3;
            offer.Image4 = dto.Image4;
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
