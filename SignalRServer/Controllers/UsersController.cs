using Microsoft.AspNetCore.Mvc;
using KoalitionServer.Requests.UserRequests;
using KoalitionServer.Models;
using Microsoft.AspNetCore.Authorization;
using KoalitionServer.Responses.UserResponses;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using KoalitionServer.Data;
using MediatR;
using KoalitionServer.Services.UserServices;

namespace KoalitionServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(UserService userService, AppDbContext appDbContext, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _context = appDbContext;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("all users"), Authorize]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("currentuser"), Authorize]
        public async Task<CurrentUserResponse> GetCurrentAuthenticatedUser()
        {
            var user = await _mediator.Send(new CurrentUserRequestDTO
                (_httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value));

            return new CurrentUserResponse()
            {
                User = new CurrentUserResponse.UserContainer()
                {
                    UserId = user.UserId,
                    Login = user.Login,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegistrationRequest regRequest)
        {
            var user = await _userService.RegisterUser(regRequest);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticateRequest authRequest)
        {
            var currentUserResponse = await _userService.Authenticate(authRequest);
            return Ok(currentUserResponse);
        }

        [HttpDelete("currentuser"), Authorize]
        public async Task<bool> DeleteCurrentUser()
        {
            return await _mediator.Send(new DeleteCurrentUserRequest
                (_httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value));
        }


    }
}
