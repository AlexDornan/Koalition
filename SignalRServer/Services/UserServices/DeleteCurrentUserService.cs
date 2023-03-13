using KoalitionServer.Data;
using KoalitionServer.Requests.UserRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KoalitionServer.Services.UserServices
{
    public class DeleteCurrentUserService : IRequestHandler<DeleteCurrentUserRequest, bool>
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
