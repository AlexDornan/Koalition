using KoalitionServer.Data;
using KoalitionServer.Models;
using KoalitionServer.Requests.UserRequests;
using KoalitionServer.Responses.UserResponses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoalitionServer.Services.UserServices
{
    public class CurrentUserService : IRequestHandler<CurrentUserRequestDTO, CurrentUserResponseDTO>
    {
        private readonly AppDbContext _context;

        public CurrentUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CurrentUserResponseDTO> Handle(CurrentUserRequestDTO request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

            if (user == null)
            {
                throw new ArgumentException("Invalid login!");
            }

            return new CurrentUserResponseDTO
            {
                UserId = user.UserId,
                Login = user.Login,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
