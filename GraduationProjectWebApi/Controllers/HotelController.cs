﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class HotelController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Hotels.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Hotels.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "HotelAdmin,SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] HotelDTO dto)
        {
            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }
            var hotel = new Hotel
            {
                Image1 = dto.Image1,
                Image2 = dto.Image2,
                Ratings = dto.Ratings,
                Details = dto.Details,
                AdminId = dto.AdminId,
                Name = dto.Name,
                Location = dto.Location,
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

        [Authorize(Roles = "HotelAdmin,SuperAdmin")]
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] HotelDTO dto)
        {
            var hotel = await _context.Hotels.FindAsync(Id);
            if (hotel is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }
            hotel.Image1 = dto.Image1;
            hotel.Image2 = dto.Image2;
            hotel.Location = dto.Location;
            hotel.Name = dto.Name;
            hotel.AdminId = dto.AdminId;
            hotel.Details = dto.Details;
            hotel.Ratings = dto.Ratings;
            try
            {
                _context.Hotels.Update(hotel);
                await _context.SaveChangesAsync();
                return Ok(hotel);
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
            var entity = await _context.Hotels.FindAsync(Id);
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
