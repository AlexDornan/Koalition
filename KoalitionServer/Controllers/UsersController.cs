using Microsoft.AspNetCore.Mvc;
using Server.Requests.UserRequests;
using Server.Models;
using Microsoft.AspNetCore.Authorization;
using Server.Responses.UserResponses;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Services.UserServices;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(UserService userService, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _context = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("allUsers"), Authorize]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("currentuser"), Authorize]
        public async Task<ActionResult<CurrentUserResponse>> GetCurrentAuthenticatedUser()
        {
            var user = await _userService.GetCurrentAuthenticatedUser();
            return Ok(user);
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegistrationRequest regRequest)
        {
            var user = await _userService.RegisterUser(regRequest);
            return Ok(user);
        }

        //add here method for updating user regRequest
        [HttpPut("update"), Authorize]
        public async Task<ActionResult<User>> UpdateUser([FromBody] RegistrationRequest updateRequest)
        {
            var user = await _userService.UpdateUser(updateRequest);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest authRequest)
        {
            var currentUserResponse = await _userService.Authenticate(authRequest);
            return Ok(currentUserResponse);
        }

        [HttpDelete("currentuser"), Authorize]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var result = await _userService.DeleteCurrentUserAsync();

            return Ok("User deleted successfully.");
        }



    }
}
