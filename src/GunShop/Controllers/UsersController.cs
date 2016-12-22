using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using GunShop.ViewModels.UsersViewModel;
using Microsoft.AspNetCore.Identity;
using GunShop.Models;

namespace GunShop.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;

        public UsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var customers = _context.Customers
                .Where(c=>!c.IsTemp)
                .ToArray();
            var model = new List<UserViewModel>();
            foreach(var c in customers)
            {
                var u = await _userManager.FindByIdAsync(c.ApplicationUserId);
                model.Add(new UserViewModel(c, u)
                {
                    Roles = await _userManager.GetRolesAsync(u)
                });
            }
            return View(model);
        }

        public async Task<IActionResult> ToggleRole(string userid, string role)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if(await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            
            return RedirectToAction("Index", "Users");
        }
    }
}