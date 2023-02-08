using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KoalitionServer.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using KoalitionServer.Data;

namespace KoalitionServer.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public readonly UserManager<User> _userManager;

        public HomeController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var messages = await _context.Messages.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                var sender = await _userManager.GetUserAsync(User);
                message.UserID = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*public ViewResult ChatDemo()
        {
            return View();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}