using KoalitionServer.Data;
using KoalitionServer.Models;
using KoalitionServer.Responses.GroupMessageResponses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Policy;

namespace KoalitionServer.Services.PrivateChatServices
{
    public class PrivateChatService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrivateChatService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PrivateMessage> SendMessage(int recipientId, string messageText, ClaimsPrincipal user)
        {
            var senderId = Convert.ToInt32(user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var sender = await _context.Users.FindAsync(senderId);
            if (sender == null)
            {
                throw new ArgumentException("Sender not found.");
            }

            var recipient = await _context.Users.SingleOrDefaultAsync(u => u.UserId == recipientId);
            if (recipient == null)
            {
                throw new ArgumentException("Recipient not found.");
            }

            var privateChat = await _context.PrivateChats.SingleOrDefaultAsync(pc =>
                pc.PrivateChatsToUsers.Any(pcu => pcu.UserId == sender.UserId) &&
                pc.PrivateChatsToUsers.Any(pcu => pcu.UserId == recipient.UserId));

            if (privateChat == null)
            {
                privateChat = new PrivateChat();
                _context.PrivateChats.Add(privateChat);

                var privateChatsToUsers = new List<PrivateChatsToUsers>
                {
                    new PrivateChatsToUsers {PrivateChat = privateChat, User = sender},
                    new PrivateChatsToUsers {PrivateChat = privateChat, User = recipient},
                };
                _context.PrivateChatsToUsers.AddRange(privateChatsToUsers);
            }

            var privateMessage = new PrivateMessage
            {
                Text = messageText,
                Time = DateTime.Now,
                PrivateChat = privateChat,
                PrivateChatId = privateChat.PrivateChatId,
                UserId = sender.UserId
            };
            _context.PrivateMessages.Add(privateMessage);
            await _context.SaveChangesAsync();

            return privateMessage;
        }

        public async Task<List<ChatMessageResponse>> GetMessagesForPrivateChat(int recipientId)
        {
            var senderId = Convert.ToInt32(_httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var sender = await _context.Users.FindAsync(senderId);
            if (sender == null)
            {
                throw new ArgumentException("Sender not found.");
            }
            var recipient = await _context.Users.SingleOrDefaultAsync(u => u.UserId == recipientId);
            if (recipient == null)
            {
                throw new ArgumentException("Recipient not found.");
            }
            var privateChat = await _context.PrivateChats.SingleOrDefaultAsync(pc =>
                pc.PrivateChatsToUsers.Any(pcu => pcu.UserId == sender.UserId) &&
                pc.PrivateChatsToUsers.Any(pcu => pcu.UserId == recipient.UserId));
            if (privateChat == null)
            {
                throw new ArgumentException("Private chat not found.");
            }
            //add here
            var messages = await _context.PrivateMessages
                .Where(pm => pm.PrivateChatId == privateChat.PrivateChatId)
                .Select(pm => new ChatMessageResponse
                {
                    Text = pm.Text,
                    Time = pm.Time,
                    UserId = pm.UserId
                })
                .ToListAsync();
            return messages;
        }

        public async Task UpdateMessage(int privateChatId, int messageId, string message)
        {
            var privateChat = await _context.PrivateChats
                .Include(gc => gc.PrivateChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .Include(gc => gc.Messages)
                .FirstOrDefaultAsync(gc => gc.PrivateChatId == privateChatId);
            if (privateChat == null)
                throw new ArgumentException($"Group chat with id {privateChatId} not found");

            var currentUser = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var privateChatUser = privateChat.PrivateChatsToUsers.FirstOrDefault(u => u.User.UserId == Convert.ToInt32(currentUser));
            if (privateChatUser == null)
                throw new ArgumentException("User is not a member of this group chat");

            var messageToUpdate = privateChat.Messages.FirstOrDefault(m => m.PrivateMessageId == messageId);
            if (messageToUpdate == null)
                throw new ArgumentException($"Message with id {messageId} not found");
            messageToUpdate.Text = message;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(int privateChatId, int messageId)
        {
            var privateChat = await _context.PrivateChats
                .Include(gc => gc.PrivateChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .Include(gc => gc.Messages)
                .FirstOrDefaultAsync(gc => gc.PrivateChatId == privateChatId);
            if (privateChat == null)
                throw new ArgumentException($"Group chat with id {privateChatId} not found");

            var currentUser = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var privateChatUser = privateChat.PrivateChatsToUsers.FirstOrDefault(u => u.User.UserId == Convert.ToInt32(currentUser));
            if (privateChatUser == null)
                throw new ArgumentException("User is not a member of this group chat");

            var message = privateChat.Messages.FirstOrDefault(m => m.PrivateMessageId == messageId);
            if (message == null)
                throw new ArgumentException($"Message with id {messageId} not found");
            _context.PrivateMessages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
