using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AdminController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Admins.ToListAsync();
            return Ok(result);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Admin admin)
        {
            var EmailCheck = await _context.Admins.AnyAsync(x => x.Email == admin.Email);
            if (EmailCheck)
            {
                return BadRequest("the email is already used");
            }
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

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Admin admin)
        {
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

        [Authorize]
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

        [HttpGet("Login/{Email}/{Password}")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var result = await _context.Admins.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
            if (result is null)
            {
                return BadRequest();
            }
            var Token = CreateToken(result);
            return Ok(Token);
        }
        private string CreateToken(Admin admin)
        {
            List<Claim> Claims = [];
            Claims.Add(new(ClaimTypes.NameIdentifier, admin.Id.ToString()));
            Claims.Add(new(ClaimTypes.Email, admin.Email));
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
