using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }

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
        public async Task<IActionResult> Add([FromBody] User user)
        {
            var EmailCheck = await _context.Users.AnyAsync(x => x.Email == user.Email);
            if (EmailCheck)
            {
                return BadRequest("the email is already used");
            }
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
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
        [HttpPut("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user is null)
            {
                return BadRequest("the email is already used");
            }

            try
            {
                _context.Users.Remove(user);
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
            Claims.Add(new(ClaimTypes.Role, "User"));
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7y*9Q!bN5@Gw@QWxsDWATFFMJ7y*9Q!bN5@Gw@QWxsDWATFFM"));
            var card = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var Token = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: card);
            var Jwt = new JwtSecurityTokenHandler().WriteToken(Token);
            return Jwt;
        }
    }
}
