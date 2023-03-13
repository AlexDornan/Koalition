using KoalitionServer.Data;
using KoalitionServer.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace KoalitionServer.Services.GroupChatServices
{
    public class GetCurrentUserGroupChatsService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserGroupChatsService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GroupChat>> GetCurrentUserGroupChats()
        {
            // Get the current user login from the HttpContext
            var currentUserLogin = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            // Use EF Core to get all group chats where the current user is a member
            var groupChats = await _context.GroupChats
                .Where(gc => gc.GroupChatsToUsers.Any(gcu => gcu.User.Login == currentUserLogin))
                .ToListAsync();

            // For each group chat, retrieve the related users and add them to the GroupChat.GroupChatsToUsers.User collection
            foreach (var groupChat in groupChats)
            {
                var groupChatToUsers = await _context.GroupChatsToUsers
                    .Include(gcu => gcu.User)
                    .Where(gcu => gcu.GroupChatId == groupChat.GroupChatId)
                    .ToListAsync();

                foreach (var groupChatToUser in groupChatToUsers)
                {
                    groupChatToUser.User.GroupChatsToUsers = null;
                }

                groupChat.GroupChatsToUsers = groupChatToUsers;
            }

            return groupChats;
        }

    }

}