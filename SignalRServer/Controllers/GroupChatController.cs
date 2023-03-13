using KoalitionServer.Data;
using KoalitionServer.Models;
using KoalitionServer.Requests.GroupChatRequests;
using KoalitionServer.Services.GroupChatServices;
using KoalitionServer.Services.UserServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Claims;

namespace KoalitionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateGroupChatService _createGroupChatService;
        private readonly AppDbContext _context;
        private readonly GetCurrentUserGroupChatsService _getCurrentUserGroupChatsService;
        private readonly DeleteGroupChatorUserService _deleteGroupChatorUserService;

        public GroupChatController(IMediator mediator, CreateGroupChatService createGroupChatService, AppDbContext appDbContext, GetCurrentUserGroupChatsService getAllGroupChatsService, DeleteGroupChatorUserService deleteGroupChatorUserService)
        {
            _mediator = mediator;
            _createGroupChatService = createGroupChatService;
            _context = appDbContext;
            _getCurrentUserGroupChatsService = getAllGroupChatsService;
            _deleteGroupChatorUserService = deleteGroupChatorUserService;
        }

        [HttpPost("createGroupChat")]
        [Authorize]
        public async Task<ActionResult<string>> CreateGroupChat(CreateGroupChatRequest request)
        {
            var chatName = await _mediator.Send(request);
            return chatName;
        }
        
        [HttpPost("adduser")]
        //[Authorize]
        public async Task<ActionResult<bool>> AddUserToGroupChat(AddUserToGroupChatRequest request)
        {
            var result = await _mediator.Send(request);
            return result;
        }

        [HttpGet("getUserChats")]
        public async Task<IActionResult> GetGroupChats()
        {
            var groupChats = await _getCurrentUserGroupChatsService.GetCurrentUserGroupChats();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedGroupChats = JsonSerializer.Serialize(groupChats, options);

            return Ok(serializedGroupChats);
        }

        [HttpDelete("{groupName}")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupChat(string groupName)
        {
            await _deleteGroupChatorUserService.DeleteGroupChatAsync(groupName, User);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupChatMember(int groupChatId, int userId)
        {
            var deleted = await _deleteGroupChatorUserService.DeleteGroupChatMemberAsync(groupChatId, userId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
