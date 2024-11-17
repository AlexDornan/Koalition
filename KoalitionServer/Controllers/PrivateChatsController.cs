using Server.Requests.PrivateChatRequests;
using Server.Responses.GroupMessageResponses;
using Server.Responses.PrivateChatResponses;
using Server.Services.GroupMessagesServices;
using Server.Services.PrivateChatServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrivateChatsController : ControllerBase
    {
        private readonly PrivateChatService _privateChatService;

        public PrivateChatsController(PrivateChatService privateChatService)
        {
            _privateChatService = privateChatService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChatMessageResponse>>> GetGroupChatMessages(int recipientId)
        {
            var messages = await _privateChatService.GetMessagesForPrivateChat(recipientId);

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

        [HttpPost("sendMessage")]
        [Authorize]
        public async Task<ActionResult<SendMessageResponse>> SendGroupMessage(int recipientId, [FromBody] PrivateMessageRequest message)
        {
            //use here SendMessage method from PrivateChatService

            await _privateChatService.SendMessage(recipientId, message.Text, User);

            var responseDto = new SendMessageResponse
            {
                Text = message.Text,
                Time = DateTime.Now,
            };

            return Ok(responseDto);
        }

        [HttpPut("{messageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateGroupMessage(int privateChatId, int messageId, [FromBody] PrivateMessageRequest message)
        {
            await _privateChatService.UpdateMessage(privateChatId, messageId, message.Text);
            return NoContent();
        }

        [HttpDelete("{messageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupMessage(int privateChatId, int messageId)
        {
            await _privateChatService.DeleteMessage(privateChatId, messageId);
            return NoContent();
        }
    }
}
