using KoalitionServer.Services.GroupMessagesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KoalitionServer.Requests.GroupMessageRequests;
using KoalitionServer.Responses.GroupMessageResponses;

namespace KoalitionServer.Controllers
{
    [ApiController]
    [Route("api/groupchats/{groupChatId}/messages")]
    public class GroupMessagesController : ControllerBase
    {
        private readonly SendGroupMessageService _sendGroupMessageService;

        public GroupMessagesController(SendGroupMessageService sendGroupMessageService)
        {
            _sendGroupMessageService = sendGroupMessageService;
        }

        [HttpPost("sendMessage")]
        [Authorize]
        public async Task<ActionResult<SendMessageResponse>> SendGroupMessage(int groupChatId, [FromBody] SendMessageRequest message)
        {
            await _sendGroupMessageService.SendGroupMessageAsync(groupChatId, message.Text, User);

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
            var messages = await _sendGroupMessageService.GetMessagesForGroupChat(groupChatId);

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
    }

    

}
