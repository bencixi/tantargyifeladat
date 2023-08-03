using InvitationManagerAPI.Data;
using InvitationManagerAPI.Models;
using InvitationManagerAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace InvitationManagerAPI.Controllers
{


    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();


        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService, DataContext invitationManagerDbContext)
        {
            _configuration = configuration;
            _userService = userService;
            _dataContext = invitationManagerDbContext;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody]UserLogin loginUser)
        {

            var dbUser = _dataContext.User.Where(u => u.Email == loginUser.Email && u.Password == loginUser.Password)
                .Select(u => new
                {
                    u.ID,
                    u.Email,
                    u.Role

                }).FirstOrDefault();

            if (dbUser == null)
            {
                return BadRequest("Nem megfelelő email cím vagy jelszó!");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Email),
                new Claim(ClaimTypes.Role, dbUser.Role)
            };

            string token = CreateToken(claims);

            return Ok(dbUser);
        }


        //Felhasználó regisztráció
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            userRequest.ID = Guid.NewGuid();

            await _dataContext.User.AddAsync(userRequest);
            await _dataContext.SaveChangesAsync();

            return Ok(userRequest);
        }

        //Összes felhasználó lekérése
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _dataContext.User.ToListAsync();

            return Ok(users);
        }


        //Felhasználó lekérése
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(x => x.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Felhasználói adatok átírása
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UserUpdate updateUser)
        {
            var user = await _dataContext.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUser.Name;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;
            user.Role = updateUser.Role;

            await _dataContext.SaveChangesAsync();
            return Ok(user);
        }


        //Felhasználó törlése
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _dataContext.User.FindAsync(id);

            if (user == null)
            {
                return NotFound(user);
            }

            _dataContext.User.Remove(user);
            await _dataContext.SaveChangesAsync();
            return Ok(user);
        }
 

        private string CreateToken(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
