using KoalitionServer.Data;
using KoalitionServer.Models;
using KoalitionServer.Requests.UserRequests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KoalitionServer.Services.UserServices
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<User> RegisterUser(RegistrationRequest regRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Email == regRequest.Email))
            {
                throw new ArgumentException("User with this email already exist!");
            }

            var newUser = new User
            {
                Login = regRequest.Login,
                Name = regRequest.Name,
                Email = regRequest.Email,
                Password = _passwordHasher.HashPassword(null, regRequest.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<string> Authenticate(AuthenticateRequest authRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == authRequest.Email);
            if (user == null)
            {
                throw new ArgumentException("Invalid email!");
            }


            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, authRequest.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid password!");
            }

            string token = CreateToken(user);

            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
