using KoalitionServer.Data;
using KoalitionServer.Responses.GroupChatResponses;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<GroupChatResponse>> GetCurrentUserGroupChats()
        {
            var currentUserLogin = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var groupChats = await _context.GroupChats
                .Where(gc => gc.GroupChatsToUsers.Any(gcu => gcu.User.Login == currentUserLogin))
                .ToListAsync();

            var groupChatDtos = groupChats.Select(gc => new GroupChatResponse
            {
                Id = gc.GroupChatId,
                Name = gc.Name
            }).ToList();

            return groupChatDtos;
        }
    }
}