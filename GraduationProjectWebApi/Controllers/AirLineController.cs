﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class AirLineController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.AirLines.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.AirLines.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "FilghtAdmin,SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AirLineDTO dto)
        {
            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }
            var airLine = new AirLine
            {
                Image = dto.Image,
                AdminId = dto.AdminId,
                Location = dto.Location,
                Name = dto.Name,
            };
            try
            {
                await _context.AddAsync(airLine);
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
        public async Task<IActionResult> Update(int Id, [FromBody] AirLineDTO dto)
        {
            var airLine = await _context.AirLines.FindAsync(Id);
            if (airLine is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }

            var admin = await _context.Admins.FindAsync(dto.AdminId);
            if (admin is null)
            {
                return BadRequest("The provided admin id dose not correspond to an object");
            }

            airLine.Name = dto.Name;
            airLine.Location = dto.Location;
            airLine.AdminId = dto.AdminId;
            airLine.Image = dto.Image;
            try
            {
                _context.AirLines.Update(airLine);
                await _context.SaveChangesAsync();
                return Ok(airLine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "FilghtAdmin,SuperAdmin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _context.AirLines.FindAsync(Id);
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
