
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
    public class UserController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Authorize(Roles ="")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _context.Users.FindAsync(Id);
            if (result is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserDTO dto)
        {
            var EmailCheck = await _context.Users.AnyAsync(x => x.Email == dto.Email);
            if (EmailCheck)
            {
                return BadRequest("the email is already used");
            }
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password,
            };
            try
            {
                await _context.AddAsync(user);
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
        public async Task<IActionResult> Update(int Id,[FromForm] UserDTO dto)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user is null)
            {
                return BadRequest("The provided id dose not correspond to an object");
            }
            user.Name = dto.Name;
            user.Password = dto.Password;
            user.Email = dto.Email;
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
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
            var entity = await _context.Users.FindAsync(Id);
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
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
            if (result is null)
            {
                return BadRequest();
            }
            var Token = CreateToken(result);
            return Ok(Token);
        }
        private string CreateToken(User user)
        {
            List<Claim> Claims = [];
            Claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
            Claims.Add(new(ClaimTypes.Email, user.Email));
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
