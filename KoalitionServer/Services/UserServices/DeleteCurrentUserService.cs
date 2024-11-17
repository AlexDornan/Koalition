using Server.Data;
using Server.Requests.UserRequests;
using Microsoft.EntityFrameworkCore;

namespace Server.Services.UserServices
{
    public class DeleteCurrentUserService
    {
        private readonly AppDbContext _context;

        public DeleteCurrentUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCurrentUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
