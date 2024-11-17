using Server.Data;
using Server.Models;
using Server.Requests.GroupChatRequests;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Server.Services.GroupChatServices
{
    public class CreateGroupChatService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateGroupChatService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Handle(CreateGroupChatRequest request, CancellationToken cancellationToken)
        {
            var login = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (login == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(l => l.Login == login, cancellationToken);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var chat = new GroupChat
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.GroupChats.Add(chat);

            var groupChatUser = new GroupChatsToUsers
            {
                GroupChat = chat,
                User = user,
                IsOwner = true
            };

            _context.GroupChatsToUsers.Add(groupChatUser);

            await _context.SaveChangesAsync();

            return chat.Name;
        }
    }
}
