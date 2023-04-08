using KoalitionServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KoalitionServer.Services.GroupChatServices
{
    public class DeleteGroupChatorUserService
    {
        private readonly AppDbContext _context;

        public DeleteGroupChatorUserService(AppDbContext context)
        {
            _context = context;            
        }

        public async Task DeleteGroupChatAsync(string groupName, ClaimsPrincipal user)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gtu => gtu.User)
                .FirstOrDefaultAsync(gc => gc.Name == groupName);

            if (groupChat == null)
            {
                throw new Exception($"Group chat with name '{groupName}' not found");
            }

            var isOwner = groupChat.GroupChatsToUsers
                .FirstOrDefault(gtu => gtu.User.Login == user.FindFirst(ClaimTypes.Name).Value && gtu.IsOwner);

            if (isOwner == null)
            {
                throw new Exception("You are not authorized to delete this group chat");
            }

            _context.GroupChats.Remove(groupChat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteGroupChatMemberAsync(int groupChatId, int userId, ClaimsPrincipal user)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.GroupChatsToUsers)
                .ThenInclude(gcu => gcu.User)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);

            if (groupChat == null)
            {
                return false;
            }

            var isOwner = groupChat.GroupChatsToUsers
                .FirstOrDefault(gtu => gtu.User.Login == user.FindFirst(ClaimTypes.Name).Value && gtu.IsOwner);

            if (isOwner == null)
            {
                throw new Exception("You are not authorized to delete this group chat");
            }

            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            var groupChatToUserToDelete = groupChat.GroupChatsToUsers.FirstOrDefault(gcu => gcu.UserId == userToDelete.UserId);

            _context.GroupChatsToUsers.Remove(groupChatToUserToDelete);
            await _context.SaveChangesAsync();          
            return true;
        }

    }
}
