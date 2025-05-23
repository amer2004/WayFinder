using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/")]
    public class AdminController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Admins.ToListAsync();
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Admins.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AdminDTO dto)
        {
            var EmailCheck = await _context.Admins.AnyAsync(x => x.Email == dto.Email);
            if (EmailCheck)
            {
                return BadRequest("the email is already used");
            }
            var admin = new Admin
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                Type = dto.Type,
            };
            try
            {
                await _context.AddAsync(admin);
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
        public async Task<IActionResult> Update(int Id, [FromBody] AdminDTO dto)
        {
            var admin = await _context.Admins.FindAsync(Id);
            if (admin is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            admin.Email = dto.Email;
            admin.LastName = dto.LastName;
            admin.PhoneNumber = dto.PhoneNumber;
            admin.FirstName = dto.FirstName;
            admin.Password = dto.Password;
            admin.Type = dto.Type;
            try
            {
                _context.Admins.Update(admin);
                await _context.SaveChangesAsync();
                return Ok(admin);
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
            var entity = await _context.Admins.FindAsync(Id);
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var result = await _context.Admins.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
            if (result is null)
            {
                return BadRequest("The email or password is incorrect");
            }
            var Token = CreateToken(result);
            return Ok(Token);
        }
        private string CreateToken(Admin admin)
        {
            List<Claim> Claims = [];
            Claims.Add(new(ClaimTypes.NameIdentifier, admin.Id.ToString()));
            Claims.Add(new(ClaimTypes.Email, admin.Email));
            Claims.Add(new(ClaimTypes.MobilePhone, admin.PhoneNumber));
            Claims.Add(new(ClaimTypes.Role, admin.Type.ToString()));
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7y*9Q!bN5@Gw@QWxsDWATFFMJ7y*is!bN5@Gw@QWxsDWATFFM"));
            var card = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var Token = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: card);
            var Jwt = new JwtSecurityTokenHandler().WriteToken(Token);
            return Jwt;
        }
    }
}
