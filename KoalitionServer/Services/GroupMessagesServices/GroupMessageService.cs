using KoalitionServer.Data;
using KoalitionServer.Models;
using KoalitionServer.Responses.GroupMessageResponses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KoalitionServer.Services.GroupMessagesServices
{
    public class GroupMessageService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupMessageService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendGroupMessageAsync(int groupChatId, string message, ClaimsPrincipal user)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);

            if (groupChat == null)
            {
                throw new Exception($"Group chat with id '{groupChatId}' not found");
            }

            var sender = groupChat.GroupChatsToUsers
                .FirstOrDefault(u => u.User.UserId == Convert.ToInt32(user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value));

            var groupMessage = new GroupMessage
            {
                Text = message,
                Time = DateTime.Now,
                GroupChatId = groupChatId,
                UserId = sender.UserId
            };

            _context.GroupMessages.Add(groupMessage);
            await _context.SaveChangesAsync();
        } 

        public async Task<List<ChatMessageResponse>> GetMessagesForGroupChat(int groupChatId)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .Include(gc => gc.Messages)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);

            if (groupChat == null)
                throw new ArgumentException($"Group chat with id {groupChatId} not found");

            var currentUser = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var groupChatUser = groupChat.GroupChatsToUsers.FirstOrDefault(u => u.User.UserId == Convert.ToInt32(currentUser));
            if (groupChatUser == null)
                throw new ArgumentException("User is not a member of this group chat");

            var messages = groupChat.Messages
                .OrderBy(m => m.Time)
                .Select(m => new ChatMessageResponse
                {
                    Text = m.Text,
                    Time = m.Time,
                    UserId = m.UserId

                })
                .ToList();

            return messages;
        }

        //add here update message method(int groupChatId, string message, ClaimsPrincipal user)
        public async Task UpdateMessage(int groupChatId, int messageId, string message)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .Include(gc => gc.Messages)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);
            if (groupChat == null)
                throw new ArgumentException($"Group chat with id {groupChatId} not found");
            var currentUser = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var groupChatUser = groupChat.GroupChatsToUsers.FirstOrDefault(u => u.User.UserId == Convert.ToInt32(currentUser));
            if (groupChatUser == null)
                throw new ArgumentException("User is not a member of this group chat");
            var messageToUpdate = groupChat.Messages.FirstOrDefault(m => m.GroupMessageId == messageId);
            if (messageToUpdate == null)
                throw new ArgumentException($"Message with id {messageId} not found");
            messageToUpdate.Text = message;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(int groupChatId, int messageId)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .Include(gc => gc.Messages)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);
            if (groupChat == null)
                throw new ArgumentException($"Group chat with id {groupChatId} not found");
            var currentUser = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var groupChatUser = groupChat.GroupChatsToUsers.FirstOrDefault(u => u.User.UserId == Convert.ToInt32(currentUser));
            if (groupChatUser == null)
                throw new ArgumentException("User is not a member of this group chat");
            var message = groupChat.Messages.FirstOrDefault(m => m.GroupMessageId == messageId);
            if (message == null)
                throw new ArgumentException($"Message with id {messageId} not found");
            _context.GroupMessages.Remove(message);
            await _context.SaveChangesAsync();
        }

    }
}
