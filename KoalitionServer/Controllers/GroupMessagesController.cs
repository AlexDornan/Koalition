using Server.Services.GroupMessagesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Requests.GroupMessageRequests;
using Server.Responses.GroupMessageResponses;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/groupchats/{groupChatId}/messages")]
    public class GroupMessagesController : ControllerBase
    {
        private readonly GroupMessageService _groupMessageService;

        public GroupMessagesController(GroupMessageService groupMessageService)
        {
            _groupMessageService = groupMessageService;
        }

        [HttpPost("sendMessage")]
        [Authorize]
        public async Task<ActionResult<SendMessageResponse>> SendGroupMessage(int groupChatId, [FromBody] SendMessageRequest message)
        {
            await _groupMessageService.SendGroupMessageAsync(groupChatId, message.Text, User);

            var responseDto = new SendMessageResponse
            {
                Text = message.Text,
                Time = DateTime.Now,
            };

            return Ok(responseDto);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChatMessageResponse>>> GetGroupChatMessages(int groupChatId)
        {
            var messages = await _groupMessageService.GetMessagesForGroupChat(groupChatId);

            var messageDtos = new List<ChatMessageResponse>();
            foreach (var message in messages)
            {
                messageDtos.Add(new ChatMessageResponse
                {
                    Text = message.Text,
                    Time = message.Time,
                    UserId = message.UserId,
                });
            }

            return Ok(messageDtos);
        }

        [HttpPut("{messageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateGroupMessage(int groupChatId, int messageId, [FromBody] SendMessageRequest message)
        {
            await _groupMessageService.UpdateMessage(groupChatId, messageId, message.Text);
            return NoContent();
        }

        [HttpDelete("{messageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupMessage(int groupChatId, int messageId)
        {
            await _groupMessageService.DeleteMessage(groupChatId, messageId);
            return NoContent();
        }

    }



}
