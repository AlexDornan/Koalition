using Server.Data;
using Server.Models;
using Server.Requests.GroupChatRequests;
using Microsoft.EntityFrameworkCore;

namespace Server.Services.GroupChatServices
{
    public class AddUserToGroupChatService
    {
        private readonly AppDbContext _context;

        public AddUserToGroupChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddUserToGroupChatRequest request, CancellationToken cancellationToken)
        {
            var chatId = request.GroupChatId;
            var userId = request.UserId;

            var chat = await _context.GroupChats
                .Include(c => c.GroupChatsToUsers)
                .ThenInclude(gctu => gctu.User)
                .FirstOrDefaultAsync(c => c.GroupChatId == chatId, cancellationToken);

            if (chat == null)
            {
                throw new InvalidOperationException("Chat not found");
            }

            if (!chat.GroupChatsToUsers.Any(gctu => gctu.UserId == userId && gctu.GroupChatId == chatId))
            {
                chat.GroupChatsToUsers.Add(new GroupChatsToUsers
                {
                    GroupChatId = chatId,
                    UserId = userId,
                    IsOwner = false
                });

                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
