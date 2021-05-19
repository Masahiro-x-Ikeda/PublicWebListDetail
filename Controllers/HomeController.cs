using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebListDetail.Data;
using WebListDetail.Models;

namespace WebListDetail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
          }

        public async Task<IActionResult> Init()
        {
            // Add Users
            if (_userManager.Users.Count() == 0)
            {
                await _userManager.CreateAsync(new IdentityUser(){Email = "admin@do-yukai.com", UserName ="admin@do-yukai.com"}, "admin-2020XXX");
                await _userManager.CreateAsync(new IdentityUser(){Email = "manager@do-yukai.com", UserName ="manager@do-yukai.com"}, "manager-2020XXX");
                await _userManager.CreateAsync(new IdentityUser(){Email = "normal@do-yukai.com", UserName ="normal@do-yukai.com"}, "normal-2020XXX");
                await _context.SaveChangesAsync();
                System.Diagnostics.Trace.WriteLine("User is created");
            }

            // User Confirmation
            var users = _userManager.Users.OrderBy(user => user.UserName);
            foreach (IdentityUser iu in users)
            {
                System.Diagnostics.Trace.WriteLine("UserId: " + iu.Id + " UserName: " + iu.UserName);

                if(!iu.EmailConfirmed)
                {
                    // iu.EmailConfirmed = true;
                    await _userManager.ConfirmEmailAsync(iu, iu.SecurityStamp);
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Trace.WriteLine("UserId: " + iu.Id + " UserName: " + iu.UserName + "...EmailCofirmed!");
                }
            }

            // Add Roles
            if (_context.Roles.Count() == 0)
            {
                var roles = new RoleStore<IdentityRole>(_context);
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
                await _roleManager.CreateAsync(new IdentityRole("Normal"));
                await _context.SaveChangesAsync();
                System.Diagnostics.Trace.WriteLine("Role is created");
            }

            // Add Roles to Users
            if (_context.UserRoles.Count() == 0)
            {
                var admin = _context.Users.First(u => u.UserName == "admin@do-yukai.com");
                var manager =  _context.Users.First(u => u.UserName == "manager@do-yukai.com");
                var normal =  _context.Users.First(u => u.UserName == "normal@do-yukai.com");
                await _userManager.AddToRoleAsync(admin, "Administrator");
                await _userManager.AddToRoleAsync(admin, "Manager");
                await _userManager.AddToRoleAsync(admin, "Normal");
                await _userManager.AddToRoleAsync(manager, "Manager");
                await _userManager.AddToRoleAsync(manager, "Normal");
                await _userManager.AddToRoleAsync(normal, "Normal");
                await _context.SaveChangesAsync();
                System.Diagnostics.Trace.WriteLine("Roles are assigned to Users");
            }

            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
