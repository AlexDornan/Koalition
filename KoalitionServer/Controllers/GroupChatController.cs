using Server.Requests.GroupChatRequests;
using Server.Services.GroupChatServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private readonly GetCurrentUserGroupChatsService _getCurrentUserGroupChatsService;
        private readonly DeleteGroupChatorUserService _deleteGroupChatorUserService;

        public GroupChatController(GetCurrentUserGroupChatsService getAllGroupChatsService, DeleteGroupChatorUserService deleteGroupChatorUserService)
        {
            _getCurrentUserGroupChatsService = getAllGroupChatsService;
            _deleteGroupChatorUserService = deleteGroupChatorUserService;
        }

        //[HttpPost("createGroupChat")]
        //[Authorize]
        //public async Task<ActionResult<string>> CreateGroupChat(CreateGroupChatRequest request)
        //{
        //    var chatName = await _mediator.Send(request);
        //    return chatName;
        //}
        
        //[HttpPost("addUser")]
        //[Authorize]
        //public async Task<ActionResult<bool>> AddUserToGroupChat(AddUserToGroupChatRequest request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result;
        //}

        [HttpGet("getChats")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserGroupChats()
        {
            var groupChatDtos = await _getCurrentUserGroupChatsService.GetCurrentUserGroupChats();
            return Ok(groupChatDtos);
        }

        [HttpPut("updateGroupChat")]
        [Authorize]
        public async Task<IActionResult> UpdateGroupChat(string groupName, [FromBody] CreateGroupChatRequest update)
        {
            await _deleteGroupChatorUserService.UpdateGroupChat(groupName, update, User);
            return NoContent();
        }

        //use here UpdateGroupChat from DeleteGroupChatorUserService


        [HttpDelete("{groupName}")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupChat(string groupName)
        {
            await _deleteGroupChatorUserService.DeleteGroupChatAsync(groupName, User);
            return NoContent();
        }

        [HttpDelete("deleteUserFromChat")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupChatMember(int groupChatId, int userId)
        {
            var deleted = await _deleteGroupChatorUserService.DeleteGroupChatMemberAsync(groupChatId, userId, User);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
